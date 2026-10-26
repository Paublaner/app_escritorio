import psycopg
from dotenv import load_dotenv
import os
from langchain_ollama import ChatOllama
import herramientas
from langgraph.prebuilt import create_react_agent
import instrucciones
from langgraph.graph import MessagesState, StateGraph, END, START
import sqlite3
from langgraph.checkpoint.sqlite import SqliteSaver
from datetime import date

funciones ={
     "obtener_cuentas": herramientas.obtener_cuentas,
     "analizar_gastos": herramientas.analizar_gastos
}

llm = ChatOllama(
    model="qwen3",
    temperature=0,
    num_ctx= 16384,
    reasoning=False #qwen3 no "piensa" en voz alta: la respuesta llega sin el bloque <think>
)

llm_with_tools = llm.bind_tools(list(funciones.values()))

def node_modelo (state: MessagesState):
    messages = state["messages"]
    #La fecha se pone en cada llamada para que no se quede en la del día en que arrancó el servidor
    sistema = instrucciones.INSTRUCCIONES.format(fecha_hoy=date.today().strftime("%d/%m/%Y"))
    lista_nueva = [("system", sistema)] + messages
    resultado= llm_with_tools.invoke(lista_nueva)
    return {"messages": [resultado]}


def node_herramientas(state: MessagesState):
    resultado = []
    ultimo = state["messages"][-1]

    for llamada in ultimo.tool_calls:
        if llamada["name"] in funciones:
            result = funciones[llamada["name"]].invoke(llamada)
            resultado.append(result)

    return {"messages": resultado}

def decidir(state: MessagesState):
    ultimo = state["messages"][-1]
    if ultimo.tool_calls:
        return "herramienta"
    return END


#Junto a este archivo, para que no dependa de desde qué carpeta se arranque el servidor
conexion = sqlite3.connect(os.path.join(os.path.dirname(__file__), "memoria.db"), check_same_thread=False)

memory = SqliteSaver(conexion)

workflow = StateGraph(MessagesState)

workflow.add_node("node_modelo", node_modelo)
workflow.add_node("node_herramientas", node_herramientas)

workflow.add_edge(START, "node_modelo")

workflow.add_conditional_edges("node_modelo", 
                                decidir,{
        "herramienta": "node_herramientas",
        END: END
    })

workflow.add_edge("node_herramientas", "node_modelo")

app = workflow.compile(checkpointer= memory) #Habria que meter el checkpointer la memory si quieres una memoria a largo plazo

def ejecutar_grafo (pregunta: str, hilo_id: str):
    #Con checkpointer, thread_id es obligatorio: cada conversación guarda su historial con su propio hilo_id
    config = {
        "configurable": {"thread_id": hilo_id},
        "recursion_limit": 40 #Para evitar que entre en un bucle infinito
    }

    respuesta = app.invoke({"messages": [("user", pregunta)]}, config)

    return respuesta["messages"][-1].content


#Solo al ejecutar main.py directamente; si otro archivo lo importa, no se lanza
if __name__ == "__main__":
    print(ejecutar_grafo("¿En qué puedo gastar menos?", "prueba"))