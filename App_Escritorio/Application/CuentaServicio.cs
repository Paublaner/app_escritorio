using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application;

public class CuentaServicio(ICuentaPersistencia cuentaPersistencia, ILogger<CuentaServicio> logger) : ICuentaServicio
{

    private readonly ICuentaPersistencia _cuentaPersistencia = cuentaPersistencia;

    private readonly ILogger<CuentaServicio> _logger = logger;

    public async Task<Result<Cuenta>> CrearAsync(string nombre, decimal saldoInicial, Cuenta.TipoCuenta tipo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            _logger.LogError("El nombre de la cuenta no puede estar vacío");
            return Result.Fail("El nombre de la cuenta no puede estar vacío");
        }

        if (saldoInicial < 0)
        {
            _logger.LogError("El saldo inicial no puede ser negativo");
            return Result.Fail("El saldo inicial no puede ser negativo");
        }

        Cuenta cuenta = new Cuenta(nombre, saldoInicial, tipo);

        var resultado = await _cuentaPersistencia.AgregarAsync(cuenta);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al crear la cuenta");
            return Result.Fail("Error al crear la cuenta");
        }

        return Result.Ok(cuenta);


    }

    public async Task<Result> EliminarAsync(int id)
    {
        var resultado = await _cuentaPersistencia.EliminarAsync(id);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al eliminar la cuenta");
            return Result.Fail("Error al eliminar la cuenta");
        }

        return Result.Ok();
    }

    public async Task<Result<Cuenta>> ObtenerPorIdAsync(int id)
    {
        var resultado = await _cuentaPersistencia.ObtenerPorIdAsync(id);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener la cuenta");
            return Result.Fail("Error al obtener la cuenta");
        }

        return Result.Ok(resultado.Value);
    }

    public async Task<Result<decimal>> ObtenerSaldoTotalAsync()
    {
        var todas = await ObtenerTodasAsync();

        if (todas.IsFailed)
        {
            return Result.Fail("Error al obtener el saldo total");
        }

        return Result.Ok(todas.Value.Sum(c => c.Saldo));

    }

    public async Task<Result<List<Cuenta>>> ObtenerTodasAsync()
    {
        var resultado = await _cuentaPersistencia.ObtenerTodasAsync();

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener las cuentas");
            return Result.Fail("Error al obtener las cuentas");
        }

        return Result.Ok(resultado.Value);
    }

    public async Task<Result> RenombrarAsync(int id, string nuevoNombre)
    {
        if (string.IsNullOrWhiteSpace(nuevoNombre))
        {
            _logger.LogError("El nombre de la cuenta no puede estar vacío");
            return Result.Fail("El nombre de la cuenta no puede estar vacío");
        }

        var resultado = await _cuentaPersistencia.ObtenerPorIdAsync(id);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener la cuenta");
            return Result.Fail("Error al obtener la cuenta");
        }

        var cuenta = resultado.Value;

        cuenta.Nombre = nuevoNombre;

        var resultadoActualizar = await _cuentaPersistencia.ActualizarAsync(cuenta);

        if (resultadoActualizar.IsFailed)
        {
            _logger.LogError("Error al renombrar la cuenta");
            return Result.Fail("Error al renombrar la cuenta");
        }

        return Result.Ok();

    }
}
