using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application;

public class MovimientoServicio(ICuentaPersistencia cuentaPersistencia, ICategoriaPersistencia categoriaPersistencia, IMovimientoPersistencia movimientoPersistencia, ILogger<MovimientoServicio> logger) : IMovimientoServicio
{
    private readonly IMovimientoPersistencia _movimientoPersistencia = movimientoPersistencia;
    private readonly ILogger<MovimientoServicio> _logger = logger;

    private readonly ICategoriaPersistencia _categoriaPersistencia = categoriaPersistencia;

    private readonly ICuentaPersistencia _cuentaPersistencia = cuentaPersistencia;

    public async Task<Result> EliminarAsync(int id)
    {
        var resultado = await _movimientoPersistencia.EliminarAsync(id);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al eliminar el movimiento con id {Id}: {Errors}", id, resultado.Errors);
        }

        return resultado;
    }

    public async Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId)
    {
        var resultado = await _movimientoPersistencia.ObtenerPorCuentaAsync(cuentaId);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener los movimientos para la cuenta {CuentaId}: {Errors}", cuentaId, resultado.Errors);
        }

        return resultado;
    }

    public async Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes)
    {
        var resultado = await _movimientoPersistencia.ObtenerPorMesAsync(anio, mes);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener los movimientos para el mes {Mes}/{Anio}: {Errors}", mes, anio, resultado.Errors);
        }

        return resultado;
    }

    

    public async Task<Result<Movimiento>> RegistrarAsync(int cuentaId, int categoriaId, decimal importe, DateTime fecha, string descripcion)
    {

        if (importe <= 0)
        {
            _logger.LogError("El importe del movimiento debe ser mayor a cero");
            return Result.Fail("El importe del movimiento debe ser mayor a cero");
        }

        var categoriaResultado = await _categoriaPersistencia.ObtenerPorIdAsync(categoriaId);

        if (categoriaResultado.IsFailed)
        {
            _logger.LogError("Error al obtener la categoría con id {CategoriaId}: {Errors}", categoriaId, categoriaResultado.Errors);
            return Result.Fail("Error al obtener la categoría");
        }

        var cuentaResultado = await _cuentaPersistencia.ObtenerPorIdAsync(cuentaId);

        if (cuentaResultado.IsFailed)
        {
            _logger.LogError("Error al obtener la cuenta con id {CuentaId}: {Errors}", cuentaId, cuentaResultado.Errors);
            return Result.Fail("Error al obtener la cuenta");
        }


        Movimiento movimiento = new Movimiento(cuentaId, categoriaId, importe,categoriaResultado.Value.Tipo , fecha, descripcion);

        var resultado = await _movimientoPersistencia.AgregarAsync(movimiento);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al registrar el movimiento para la cuenta {CuentaId}: {Errors}", cuentaId, resultado.Errors);
        }

        decimal nuevoSaldo = cuentaResultado.Value.Saldo;

        if (cuentaResultado.Value.Tipo == Cuenta.TipoCuenta.Credito)
        {
            _logger.LogError("No se pueden registrar movimientos en cuentas de tipo crédito");
            return Result.Fail("No se pueden registrar movimientos en cuentas de tipo crédito");
        }
        else if (categoriaResultado.Value.Tipo == Categoria.TipoMovimiento.Gasto && cuentaResultado.Value.Saldo < importe)
        {
            _logger.LogError("No hay suficiente saldo en la cuenta {CuentaId} para registrar el movimiento", cuentaId);
            return Result.Fail("No hay suficiente saldo en la cuenta");
        }
        else if (categoriaResultado.Value.Tipo == Categoria.TipoMovimiento.Gasto)
        {
            nuevoSaldo -= importe;
        }
        else if (categoriaResultado.Value.Tipo == Categoria.TipoMovimiento.Ingreso)
        {
            nuevoSaldo += importe;
        }
        

        cuentaResultado.Value.Saldo = nuevoSaldo;

        var resultadoActualizarSaldo = await _cuentaPersistencia.ActualizarAsync(cuentaResultado.Value);
            
        if (resultadoActualizarSaldo.IsFailed)
        {
            _logger.LogError("Error al actualizar el saldo de la cuenta {CuentaId}: {Errors}", cuentaId, resultadoActualizarSaldo.Errors);
            return Result.Fail("Error al actualizar el saldo de la cuenta");
        }

        return Result.Ok(movimiento);
    }
}
