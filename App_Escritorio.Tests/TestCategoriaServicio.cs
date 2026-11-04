using App_Escritorio.Application;
using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace App_Escritorio.Tests;

public class TestCategoriaServicio
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CrearAsync_NombreVacio_DevuelveError(string nombre)
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync(nombre, Categoria.TipoMovimiento.Gasto);

        mockPersistencia.Verify(p => p.ObtenerTodasAsync(), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El nombre de la categoría es obligatorio");
    }

    [Fact]
    public async Task CrearAsync_TipoInvalido_DevuelveError()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Comida", (Categoria.TipoMovimiento)99);

        mockPersistencia.Verify(p => p.ObtenerTodasAsync(), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "El tipo de movimiento de la categoría es obligatorio");
    }

    [Fact]
    public async Task CrearAsync_FallaObtenerCategorias_DevuelveError()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Fail<List<Categoria>>("Fallo de BD"));

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Comida", Categoria.TipoMovimiento.Gasto);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Categoria>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener las categorías");
    }

    [Fact]
    public async Task CrearAsync_CategoriaDuplicada_DevuelveError()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        var categorias = new List<Categoria>
        {
            new Categoria("Comida", Categoria.TipoMovimiento.Gasto)
        };

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(categorias));

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("COMIDA", Categoria.TipoMovimiento.Gasto);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Categoria>()), Times.Never);

        Assert.Contains(resultado.Errors, e => e.Message == "Ya existe una categoría con el mismo nombre y tipo");
    }

    [Fact]
    public async Task CrearAsync_MismoNombreOtroTipo_CreaCategoria()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        var categorias = new List<Categoria>
        {
            new Categoria("Comida", Categoria.TipoMovimiento.Gasto)
        };

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(categorias));
        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Categoria>())).ReturnsAsync(Result.Ok());

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Comida", Categoria.TipoMovimiento.Ingreso);

        mockPersistencia.Verify(p => p.AgregarAsync(It.Is<Categoria>(c => c.Nombre == "Comida" && c.Tipo == Categoria.TipoMovimiento.Ingreso)), Times.Once);

        Assert.True(resultado.IsSuccess);
    }

    [Fact]
    public async Task CrearAsync_FallaAgregar_DevuelveError()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(new List<Categoria>()));
        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Categoria>())).ReturnsAsync(Result.Fail("Fallo de BD"));

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Comida", Categoria.TipoMovimiento.Gasto);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al crear la categoría");
    }

    [Fact]
    public async Task CrearAsync_DatosValidos_DevuelveCategoria()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(new List<Categoria>()));
        mockPersistencia.Setup(p => p.AgregarAsync(It.IsAny<Categoria>())).ReturnsAsync(Result.Ok());

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.CrearAsync("Comida", Categoria.TipoMovimiento.Gasto);

        mockPersistencia.Verify(p => p.AgregarAsync(It.IsAny<Categoria>()), Times.Once);

        Assert.True(resultado.IsSuccess);
        Assert.Equal("Comida", resultado.Value.Nombre);
        Assert.Equal(Categoria.TipoMovimiento.Gasto, resultado.Value.Tipo);
    }

    [Fact]
    public async Task ObtenerPorTipoAsync_VariasCategorias_DevuelveSoloEseTipo()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        var categorias = new List<Categoria>
        {
            new Categoria("Comida", Categoria.TipoMovimiento.Gasto),
            new Categoria("Ocio", Categoria.TipoMovimiento.Gasto),
            new Categoria("Nómina", Categoria.TipoMovimiento.Ingreso)
        };

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Ok(categorias));

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerPorTipoAsync(Categoria.TipoMovimiento.Gasto);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(2, resultado.Value.Count);
        Assert.All(resultado.Value, c => Assert.Equal(Categoria.TipoMovimiento.Gasto, c.Tipo));
    }

    [Fact]
    public async Task ObtenerPorTipoAsync_FallaPersistencia_DevuelveError()
    {
        var mockPersistencia = new Mock<ICategoriaPersistencia>();
        var mockLogger = new Mock<ILogger<CategoriaServicio>>();

        mockPersistencia.Setup(p => p.ObtenerTodasAsync()).ReturnsAsync(Result.Fail<List<Categoria>>("Fallo de BD"));

        var servicio = new CategoriaServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerPorTipoAsync(Categoria.TipoMovimiento.Gasto);

        Assert.Contains(resultado.Errors, e => e.Message == "Error al obtener las categorías");
    }
}
