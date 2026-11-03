using App_Escritorio.Application;
using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using Castle.DynamicProxy.Generators;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace App_Escritorio.Tests;

public class TestMovimientoServicio
{

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task RegistrarAsync_ImporteNoPositivo_DevuelveError(int importe)
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, importe, DateTime.Now, "Descripción de prueba");

        mockCategoriaPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El importe del movimiento debe ser mayor a cero");

    }


    [Fact]
    public async Task RegistrarAsync_CategoriaNoExiste_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Categoria>("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 100, DateTime.Now, "Descripción de prueba");

        mockCuentaPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener la categoría");
    }

    [Fact]
    public async Task RegistrarAsync_CuentaNoExiste_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Cuenta>("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 100, DateTime.Now, "Descripción de prueba");

        mockMovimientoPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Movimiento>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener la cuenta");
    }

    [Fact]
    public async Task RegistrarAsync_GastoSinSaldo_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 50, Cuenta.TipoCuenta.Banco)));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 100, DateTime.Now, "Descripción de prueba");

        mockMovimientoPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Movimiento>()), Times.Never);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "No hay suficiente saldo en la cuenta");
    }

    [Fact]
    public async Task RegistrarAsync_GastoSinSaldoEnCredito_Registra()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Tarjeta", 50, Cuenta.TipoCuenta.Credito)));
        mockMovimientoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Movimiento>())).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 100, DateTime.Now, "Descripción de prueba");

        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == -50m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task RegistrarAsync_FallaAgregar_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Movimiento>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 30, DateTime.Now, "Descripción de prueba");

        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al registrar el movimiento");
    }

    [Fact]
    public async Task RegistrarAsync_FallaActualizarSaldo_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Movimiento>())).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 30, DateTime.Now, "Descripción de prueba");

        Assert.Contains(resultado.Errors, e => e.Message == "Error al actualizar el saldo de la cuenta");
    }

    [Fact]
    public async Task RegistrarAsync_Gasto_RestaSaldo()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Movimiento>())).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 30.25m, DateTime.Now, "Descripción de prueba");

        mockMovimientoPersistencia.Verify(p => p.AgregarAsync(It.Is<Movimiento>(m => m.Importe == 30.25m && m.Tipo == Categoria.TipoMovimiento.Gasto)), Times.Once);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == 69.75m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task RegistrarAsync_Ingreso_SumaSaldo()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Nómina", Categoria.TipoMovimiento.Ingreso)));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Movimiento>())).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.RegistrarAsync(1, 1, 30.25m, DateTime.Now, "Descripción de prueba");

        mockMovimientoPersistencia.Verify(p => p.AgregarAsync(It.Is<Movimiento>(m => m.Importe == 30.25m && m.Tipo == Categoria.TipoMovimiento.Ingreso)), Times.Once);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == 130.25m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task EliminarAsync_MovimientoNoExiste_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Movimiento>("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockCuentaPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener el movimiento");

    }

    [Fact]
    public async Task EliminarAsync_IngresoDejaSaldoNegativo_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 100, Categoria.TipoMovimiento.Ingreso, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 50, Cuenta.TipoCuenta.Banco)));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockMovimientoPersistencia.Verify(p => p.EliminarAsync(It.IsAny<int>()), Times.Never);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "No se puede eliminar el movimiento: la cuenta quedaría con saldo negativo");
    }

    [Fact]
    public async Task EliminarAsync_IngresoDejaSaldoNegativoEnCredito_Elimina()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 100, Categoria.TipoMovimiento.Ingreso, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Tarjeta", 50, Cuenta.TipoCuenta.Credito)));
        mockMovimientoPersistencia.Setup(p => p.EliminarAsync(1)).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == -50m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task EliminarAsync_FallaEliminar_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 30, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.EliminarAsync(1)).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Cuenta>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al eliminar el movimiento");
    }

    [Fact]
    public async Task EliminarAsync_FallaActualizarSaldo_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 30, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.EliminarAsync(1)).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al actualizar el saldo de la cuenta");
    }

    [Fact]
    public async Task EliminarAsync_Gasto_SumaSaldo()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 30.25m, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.EliminarAsync(1)).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockMovimientoPersistencia.Verify(p => p.EliminarAsync(1), Times.Once);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == 130.25m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task EliminarAsync_Ingreso_RestaSaldo()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(1)).ReturnsAsync(Result.Ok(new Movimiento(7, 1, 30.25m, Categoria.TipoMovimiento.Ingreso, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(7)).ReturnsAsync(Result.Ok(new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Banco)));
        mockMovimientoPersistencia.Setup(p => p.EliminarAsync(1)).ReturnsAsync(Result.Ok());
        mockCuentaPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Cuenta>())).ReturnsAsync(Result.Ok());

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockMovimientoPersistencia.Verify(p => p.EliminarAsync(1), Times.Once);
        mockCuentaPersistencia.Verify(p => p.ActualizarAsync(It.Is<Cuenta>(c => c.Saldo == 69.75m)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task EliminarAsync_CuentaNoExiste_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<MovimientoServicio>>();
        var mockCuentaPersistencia = new Mock<ICuentaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Movimiento(1, 1, 100, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Descripción de prueba")));
        mockCuentaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Cuenta>("Fallo de BD"));

        var servicio = new MovimientoServicio(mockCuentaPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarAsync(1);

        mockMovimientoPersistencia.Verify(p => p.EliminarAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener la cuenta");

    }

}
