using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Application;
using Microsoft.Extensions.Logging;
using App_Escritorio.Domain.Model;
using FluentResults;
namespace App_Escritorio.Test;

public class TestCuentaServicio
{
    [Fact]
    public async Task CrearAsync_NombreVacio_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("", 100, Cuenta.TipoCuenta.Ahorro);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El nombre de la cuenta no puede estar vacío");
    }

    [Fact]
    public async Task CrearAsync_SaldoNegativo_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Cuenta de prueba", -100, Cuenta.TipoCuenta.Ahorro);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El saldo inicial no puede ser negativo");
    }

    [Fact]
    public async Task CrearAsync_FallaPersistencia_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);
        
        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var resultado = await servicio.CrearAsync("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);

        Assert.Contains(resultado.Errors, e => e.Message == "Fallo de BD");
    }

    [Fact]
    public async Task CrearAsync_DatosValidos_DevuelveCuenta()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Cuenta>()), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

}
