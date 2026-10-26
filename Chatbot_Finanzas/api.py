import logging

from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field
from main import ejecutar_grafo
logger = logging.getLogger("chatbot")

app = FastAPI(title="ChatBot")


class PeticionChat(BaseModel):
    #FastAPI rechaza sola (422) las preguntas vacías o de más de 2000 caracteres
    pregunta: str = Field(min_length=1, max_length=2000)
    #Identifica la conversación: con el mismo hilo_id el agente recuerda lo hablado
    hilo_id: str = Field(min_length=1, max_length=100)


class RespuestaChat(BaseModel):
    respuesta: str


@app.post("/chat", response_model=RespuestaChat)
def chat(peticion: PeticionChat) -> RespuestaChat:
    try:
        mensaje = ejecutar_grafo(peticion.pregunta, peticion.hilo_id)
    except Exception as e:
        logger.exception("Error al ejecutar el agente (hilo %s)", peticion.hilo_id)
        raise HTTPException(status_code=503, detail="El asistente no está disponible en este momento") from e

    return RespuestaChat(respuesta=mensaje)
