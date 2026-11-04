using App_Escritorio.Application;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Tests;

public class TestAsistenteServicio
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public async Task PreguntarAsync_PreguntaVacia_DevuelveError(string pregunta)
    {
        var mockAsistenteIA = new Mock<IAsistenteIA>();
        var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<AsistenteServicio>>();

        var servicio = new AsistenteServicio(mockAsistenteIA.Object, mockLogger.Object);

        var resultado = await servicio.PreguntarAsync(pregunta, "hilo123");

        Assert.Contains("La pregunta no puede estar vacía.", resultado.Errors[0].Message);
    }

    [Fact]
    public async Task PreguntarAsync_RespuestaCorrecta_DevuelveTexto()
    {
        var mockAsistenteIA = new Mock<IAsistenteIA>();
        var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<AsistenteServicio>>();

        mockAsistenteIA.Setup(a => a.PreguntarAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Result.Ok("Respuesta de prueba"));

        var servicio = new AsistenteServicio(mockAsistenteIA.Object, mockLogger.Object);

        var resultado = await servicio.PreguntarAsync("¿Cuál es la capital de Francia?", "hilo123");

        Assert.Contains("Respuesta de prueba", resultado.Value);
    }

    [Fact]
    public async Task PreguntarAsync_LanzaExcepcion_DevuelveError()
    {
        var mockAsistenteIA = new Mock<IAsistenteIA>();
        var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<AsistenteServicio>>();

        mockAsistenteIA.Setup(a => a.PreguntarAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("Error de prueba"));

        var servicio = new AsistenteServicio(mockAsistenteIA.Object, mockLogger.Object);

        var resultado = await servicio.PreguntarAsync("¿Cuál es la capital de Francia?", "hilo123");

        Assert.Contains("Error al procesar la pregunta.", resultado.Errors[0].Message);
    }

}
