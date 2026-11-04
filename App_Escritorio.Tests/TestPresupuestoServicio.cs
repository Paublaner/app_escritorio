using App_Escritorio.Application;
using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace App_Escritorio.Tests;

public class TestPresupuestoServicio
{
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task DefinirAsync_LimiteNoPositivo_DevuelveError(int limite)
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, limite, 2026, 10);

        mockCategoriaPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El límite del presupuesto debe ser mayor a cero");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public async Task DefinirAsync_MesInvalido_DevuelveError(int mes)
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, mes);

        mockCategoriaPersistencia.Verify(p => p.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El mes debe estar entre 1 y 12");
    }

    [Fact]
    public async Task DefinirAsync_CategoriaNoExiste_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Fail<Categoria>("Fallo de BD"));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, 10);

        mockPresupuestoPersistencia.Verify(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener la categoría");
    }

    [Fact]
    public async Task DefinirAsync_CategoriaDeIngreso_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Nómina", Categoria.TipoMovimiento.Ingreso)));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, 10);

        mockPresupuestoPersistencia.Verify(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Solo se pueden definir presupuestos para categorías de gasto");
    }

    [Fact]
    public async Task DefinirAsync_FallaObtenerExistente_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockPresupuestoPersistencia.Setup(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Fail<Presupuesto?>("Fallo de BD"));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, 10);

        mockPresupuestoPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Presupuesto>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener el presupuesto");
    }

    [Fact]
    public async Task DefinirAsync_SinPresupuesto_LoCrea()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockPresupuestoPersistencia.Setup(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok<Presupuesto?>(null));
        mockPresupuestoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Presupuesto>())).ReturnsAsync(Result.Ok());

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, 10);

        mockPresupuestoPersistencia.Verify(p => p.AgregarAsync(It.Is<Presupuesto>(x => x.CategoriaId == 1 && x.Limite == 500 && x.Anio == 2026 && x.Mes == 10)), Times.Once);
        mockPresupuestoPersistencia.Verify(p => p.ActualizarAsync(It.IsAny<Presupuesto>()), Times.Never);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(500, resultado.Value.Limite);
    }

    [Fact]
    public async Task DefinirAsync_FallaAgregar_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockPresupuestoPersistencia.Setup(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok<Presupuesto?>(null));
        mockPresupuestoPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Presupuesto>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 500, 2026, 10);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al crear el presupuesto");
    }

    [Fact]
    public async Task DefinirAsync_PresupuestoExistente_ActualizaLimite()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockPresupuestoPersistencia.Setup(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok<Presupuesto?>(new Presupuesto(1, 100, 2026, 10)));
        mockPresupuestoPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Presupuesto>())).ReturnsAsync(Result.Ok());

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 200, 2026, 10);

        mockPresupuestoPersistencia.Verify(p => p.ActualizarAsync(It.Is<Presupuesto>(x => x.Limite == 200)), Times.Once);
        mockPresupuestoPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Presupuesto>()), Times.Never);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(200, resultado.Value.Limite);
    }

    [Fact]
    public async Task DefinirAsync_FallaActualizar_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockCategoriaPersistencia.Setup(p => p.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(Result.Ok(new Categoria("Comida", Categoria.TipoMovimiento.Gasto)));
        mockPresupuestoPersistencia.Setup(p => p.ObtenerPorCategoriaYMesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok<Presupuesto?>(new Presupuesto(1, 100, 2026, 10)));
        mockPresupuestoPersistencia.Setup(p => p.ActualizarAsync(It.IsAny<Presupuesto>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.DefinirAsync(1, 200, 2026, 10);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al actualizar el presupuesto");
    }

    [Fact]
    public async Task ObtenerGastadoAsync_VariosMovimientos_SumaSoloGastosDeCategoria()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        var movimientos = new List<Movimiento>
        {
            new Movimiento(1, 1, 30, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Gasto de la categoría"),
            new Movimiento(1, 1, 20, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Otro gasto de la categoría"),
            new Movimiento(1, 1, 100, Categoria.TipoMovimiento.Ingreso, DateTime.Now, "Ingreso de la categoría"),
            new Movimiento(1, 2, 50, Categoria.TipoMovimiento.Gasto, DateTime.Now, "Gasto de otra categoría")
        };

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorMesAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok(movimientos));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerGastadoAsync(1, 2026, 10);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(50, resultado.Value);
    }

    [Fact]
    public async Task ObtenerGastadoAsync_FallaPersistencia_DevuelveError()
    {
        var mockLogger = new Mock<ILogger<PresupuestoServicio>>();
        var mockPresupuestoPersistencia = new Mock<IPresupuestoPersistencia>();
        var mockCategoriaPersistencia = new Mock<ICategoriaPersistencia>();
        var mockMovimientoPersistencia = new Mock<IMovimientoPersistencia>();

        mockMovimientoPersistencia.Setup(p => p.ObtenerPorMesAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Fail<List<Movimiento>>("Fallo de BD"));

        var servicio = new PresupuestoServicio(mockPresupuestoPersistencia.Object, mockCategoriaPersistencia.Object, mockMovimientoPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerGastadoAsync(1, 2026, 10);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al calcular lo gastado");
    }
}
