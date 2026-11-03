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
namespace App_Escritorio.Tests;

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
        
        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Fail("Error al crear la cuenta"));

        var resultado = await servicio.CrearAsync("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al crear la cuenta");
    }

    [Fact]
    public async Task CrearAsync_DatosValidos_DevuelveCuenta()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);

        mockPersistencia.Verify(p => p.AgregarAsync(It.Is<Cuenta>(c => c.Nombre == "Cuenta de prueba" && c.Saldo == 100 && c.Tipo == Cuenta.TipoCuenta.Ahorro)), Times.Once);

        Assert.True(resultado.IsSuccess);
        Assert.Equal("Cuenta de prueba", resultado.Value.Nombre);
        Assert.Equal(100m, resultado.Value.Saldo);
        Assert.Equal(Cuenta.TipoCuenta.Ahorro, resultado.Value.Tipo);
    }


    [Fact]
    public async Task RenombrarAsync_NombreVacio_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RenombrarAsync(1, "");

        mockPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El nombre de la cuenta no puede estar vacío");

    }

    [Fact]
    public async Task RenombrarAsync_CuentaNoExiste_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Cuenta>("Cuenta no encontrada"));

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RenombrarAsync(1, "Nuevo nombre");

        mockPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener la cuenta");    
    }

    [Fact]
    public async Task RenombrarAsync_FallaActualizar_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro)));
        mockPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RenombrarAsync(1, "Nuevo nombre");

        Assert.Contains(resultado.Errors, e => e.Message == "Error al renombrar la cuenta");
    }

    [Fact]
    public async Task RenombrarAsync_DatosValidos_CambiaNombre()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro)));
        mockPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RenombrarAsync(1, "Nuevo nombre");

        mockPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Nombre == "Nuevo nombre")), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task ObtenerSaldoTotalAsync_VariasCuentas_DevuelveSuma()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(new List<Cuenta>
        {
            new Cuenta("Cuenta 1", 100, Cuenta.TipoCuenta.Ahorro),
            new Cuenta("Cuenta 2", 250.50m, Cuenta.TipoCuenta.Banco),
            new Cuenta("Cuenta 3", 300, Cuenta.TipoCuenta.Ahorro)
        }));

        var resultado = await servicio.ObtenerSaldoTotalAsync();

        Assert.True(resultado.IsSuccess);
        Assert.Equal(650.50m, resultado.Value);
    }

    [Fact]
    public async Task ObtenerSaldoTotalAsync_SinCuentas_DevuelveCero()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(new List<Cuenta>()));

        var resultado = await servicio.ObtenerSaldoTotalAsync();

        Assert.True(resultado.IsSuccess);
        Assert.Equal(0m, resultado.Value);
    }


    [Fact]
    public async Task ObtenerSaldoTotalAsync_FallaPersistencia_DevuelveError()
    {
        var mockPersistencia = new Mock<ICuentaPersistencia>();
        var mockLogger = new Mock<ILogger<CuentaServicio>>();

        var servicio = new CuentaServicio(mockPersistencia.Object, mockLogger.Object);

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Fail<List<Cuenta>>("Fallo de BD"));

        var resultado = await servicio.ObtenerSaldoTotalAsync();

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener el saldo total");
    }



}
