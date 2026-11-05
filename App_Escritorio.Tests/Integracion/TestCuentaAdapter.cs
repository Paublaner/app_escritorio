using App_Escritorio.Domain.Model;
using App_Escritorio.Infrastructure.Driven.Database.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Tests.Integracion;

[Trait("Tipo", "Integracion")]
public class TestCuentaAdapter(PostgresFixture fixture): IClassFixture<PostgresFixture>
{
    private readonly PostgresFixture _fixture = fixture;

    [Fact]
    public async Task AgregarAsync_DatosValidos_GuardaCuenta()
    {
        var cuenta = new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultado = await adapter.AgregarAsync(cuenta);

        Assert.True(resultado.IsSuccess);
        Assert.True(cuenta.Id > 0);

        using var contextoLectura = _fixture.CrearContexto();
        var cuentaGuardada = await contextoLectura.Cuentas.FindAsync(cuenta.Id);

        Assert.NotNull(cuentaGuardada);
        Assert.Equal(cuenta.Nombre, cuentaGuardada.Nombre);
        Assert.Equal(cuenta.Saldo, cuentaGuardada.Saldo);
        Assert.Equal(cuenta.Tipo, cuentaGuardada.Tipo);
    }

    [Fact]
    public async Task ActualizarAsync_CuentaExiste_GuardaCambios()
    {
        var cuenta = new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultado = await adapter.AgregarAsync(cuenta);
        Assert.True(resultado.IsSuccess);

        var nuevaCuenta = new Cuenta("Cuenta actualizada", 200, Cuenta.TipoCuenta.Banco);

        nuevaCuenta.Id = cuenta.Id;

        using var contextoActualizar = _fixture.CrearContexto();
        var adapterActualizar = new CuentaAdapter(contextoActualizar);

        var resultadoActualizar = await adapterActualizar.ActualizarAsync(nuevaCuenta);

        Assert.True(resultadoActualizar.IsSuccess);

        using var contextoLectura = _fixture.CrearContexto();
        var cuentaActualizada = await contextoLectura.Cuentas.FindAsync(nuevaCuenta.Id);

        Assert.NotNull(cuentaActualizada);
        Assert.Equal(nuevaCuenta.Nombre, cuentaActualizada.Nombre);
        Assert.Equal(nuevaCuenta.Saldo, cuentaActualizada.Saldo);
        Assert.Equal(nuevaCuenta.Tipo, cuentaActualizada.Tipo);
    }

    [Fact]
    public async Task ActualizarAsync_CuentaNoExiste_DevuelveError()
    {
        var cuenta = new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        cuenta.Id = 9999;

        var resultadoActualizar = await adapter.ActualizarAsync(cuenta);

        Assert.Contains(resultadoActualizar.Errors, e => e.Message == "Cuenta no encontrada");

    }

    [Fact]
    public async Task EliminarAsync_CuentaExiste_LaElimina()
    {
        var cuenta = new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultadoAgregar = await adapter.AgregarAsync(cuenta);
        Assert.True(resultadoAgregar.IsSuccess);

        using var contextoEliminar = _fixture.CrearContexto();
        var adapterEliminar = new CuentaAdapter(contextoEliminar);

        var resultadoEliminar = await adapterEliminar.EliminarAsync(cuenta.Id);
        Assert.True(resultadoEliminar.IsSuccess);

        using var contextoLectura = _fixture.CrearContexto();
        var cuentaEliminada = await contextoLectura.Cuentas.FindAsync(cuenta.Id);

        Assert.Null(cuentaEliminada);
    }

    [Fact]
    public async Task EliminarAsync_CuentaNoExiste_DevuelveError()
    {
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultadoEliminar = await adapter.EliminarAsync(9999);

        Assert.Contains(resultadoEliminar.Errors, e => e.Message == "Cuenta no encontrada");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task EliminarAsync_IdNoValido_DevuelveError(int id)
    {
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultadoEliminar = await adapter.EliminarAsync(id);

        Assert.Contains(resultadoEliminar.Errors, e => e.Message == "Id no válido");

    }

    [Fact]
    public async Task ObtenerPorIdAsync_CuentaExiste_DevuelveCuenta()
    {
        var cuenta = new Cuenta("Cuenta de prueba", 100, Cuenta.TipoCuenta.Ahorro);
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultadoAgregar = await adapter.AgregarAsync(cuenta);
        Assert.True(resultadoAgregar.IsSuccess);

        using var contextoLectura = _fixture.CrearContexto();
        var adapterLectura = new CuentaAdapter(contextoLectura);

        var resultado = await adapterLectura.ObtenerPorIdAsync(cuenta.Id);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(cuenta.Id, resultado.Value.Id);
        Assert.Equal(cuenta.Nombre, resultado.Value.Nombre);
        Assert.Equal(cuenta.Saldo, resultado.Value.Saldo);
        Assert.Equal(cuenta.Tipo, resultado.Value.Tipo);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_CuentaNoExiste_DevuelveError()
    {
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultado = await adapter.ObtenerPorIdAsync(9999);

        Assert.Contains(resultado.Errors, e => e.Message == "Cuenta no encontrada");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ObtenerPorIdAsync_IdNoValido_DevuelveError(int id)
    {
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultado = await adapter.ObtenerPorIdAsync(id);

        Assert.Contains(resultado.Errors, e => e.Message == "Id no válido");
    }

    [Fact]
    public async Task ObtenerTodasAsync_DatosValidos()
    {
        var cuenta1 = new Cuenta("Cuenta 1", 100, Cuenta.TipoCuenta.Ahorro);
        var cuenta2 = new Cuenta("Cuenta 2", 200, Cuenta.TipoCuenta.Banco);

        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var resultadoAgregar1 = await adapter.AgregarAsync(cuenta1);
        var resultadoAgregar2 = await adapter.AgregarAsync(cuenta2);
        Assert.True(resultadoAgregar1.IsSuccess);
        Assert.True(resultadoAgregar2.IsSuccess);


        using var contextoLectura = _fixture.CrearContexto();
        var adapterLectura = new CuentaAdapter(contextoLectura);
        var resultadoLectura = await adapterLectura.ObtenerTodasAsync();

        Assert.True(resultadoLectura.IsSuccess);
        Assert.Contains(resultadoLectura.Value, c => c.Id == cuenta1.Id && c.Nombre == cuenta1.Nombre && c.Saldo == cuenta1.Saldo && c.Tipo == cuenta1.Tipo);
        Assert.Contains(resultadoLectura.Value, c => c.Id == cuenta2.Id && c.Nombre == cuenta2.Nombre && c.Saldo == cuenta2.Saldo && c.Tipo == cuenta2.Tipo);
    }

    [Fact]
    public async Task EliminarAsync_ConMovimientos()
    {
        using var contexto = _fixture.CrearContexto();
        var adapter = new CuentaAdapter(contexto);

        var cuenta = new Cuenta("Cuenta con movimientos", 100, Cuenta.TipoCuenta.Ahorro);
        var resultadoAgregar = await adapter.AgregarAsync(cuenta);


        var categoria = new Categoria("Categoria de prueba", Categoria.TipoMovimiento.Gasto);
        var resultadoAgregarCategoria = await new CategoriaAdapter(contexto).AgregarAsync(categoria);

        var movimiento = new Movimiento(cuenta.Id, categoria.Id, 50, categoria.Tipo, DateTime.Now, "Movimiento de prueba");
        var resultadoAgregarMovimiento = await new MovimientoAdapter(contexto).AgregarAsync(movimiento);

        Assert.True(resultadoAgregar.IsSuccess);
        Assert.True(resultadoAgregarCategoria.IsSuccess);
        Assert.True(resultadoAgregarMovimiento.IsSuccess);

        using var contextoEliminar = _fixture.CrearContexto();
        var adapterEliminar = new CuentaAdapter(contextoEliminar);

        var resultado = await adapterEliminar.EliminarAsync(cuenta.Id);

        Assert.True(resultado.IsFailed);
        Assert.Contains(resultado.Errors, e => e.Message.StartsWith("Error al eliminar la cuenta"));

        using var contextoLectura = _fixture.CrearContexto();
        var cuentaExistente = await contextoLectura.Cuentas.FindAsync(cuenta.Id);

        Assert.NotNull(cuentaExistente);

    }
}
    


