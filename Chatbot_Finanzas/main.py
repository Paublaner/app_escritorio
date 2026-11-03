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
     "analizar_gastos": herramientas.analizar_gastos,
     "buscar_ofertas": herramientas.buscar_ofertas
}

llm = ChatOllama(
    model="qwen3",
    temperature=0,
    num_ctx= 16384,
    reasoning=False 
)

llm_with_tools = llm.bind_tools(list(funciones.values()))

def node_modelo (state: MessagesState):
    messages = state["messages"]
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

app = workflow.compile(checkpointer= memory)

def ejecutar_grafo (pregunta: str, hilo_id: str):

    config = {
        "configurable": {"thread_id": hilo_id},
        "recursion_limit": 40 
    }

    respuesta = app.invoke({"messages": [("user", pregunta)]}, config)

    return respuesta["messages"][-1].content
