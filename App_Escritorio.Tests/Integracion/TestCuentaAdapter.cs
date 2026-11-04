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
}
    


