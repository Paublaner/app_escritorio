using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application;

public class PresupuestoServicio(IPresupuestoPersistencia presupuestoPersistencia, ICategoriaPersistencia categoriaPersistencia, IMovimientoPersistencia movimientoPersistencia, ILogger<PresupuestoServicio> logger) : IPresupuestoServicio
{

    private readonly IPresupuestoPersistencia _presupuestoPersistencia = presupuestoPersistencia;

    private readonly ICategoriaPersistencia _categoriaPersistencia = categoriaPersistencia;

    private readonly IMovimientoPersistencia _movimientoPersistencia = movimientoPersistencia;

    private readonly ILogger<PresupuestoServicio> _logger = logger;

    public async Task<Result<Presupuesto>> DefinirAsync(int categoriaId, decimal limite, int anio, int mes)
    {
        if (limite <= 0)
        {
            _logger.LogError("El límite del presupuesto debe ser mayor a cero");
            return Result.Fail("El límite del presupuesto debe ser mayor a cero");
        }

        if (mes < 1 || mes > 12)
        {
            _logger.LogError("El mes {Mes} no es válido", mes);
            return Result.Fail("El mes debe estar entre 1 y 12");
        }

        var categoriaResultado = await _categoriaPersistencia.ObtenerPorIdAsync(categoriaId);

        if (categoriaResultado.IsFailed)
        {
            _logger.LogError("Error al obtener la categoría con id {CategoriaId}: {Errors}", categoriaId, categoriaResultado.Errors);
            return Result.Fail("Error al obtener la categoría");
        }

        if (categoriaResultado.Value.Tipo != Categoria.TipoMovimiento.Gasto)
        {
            _logger.LogError("La categoría {CategoriaId} no es de gasto", categoriaId);
            return Result.Fail("Solo se pueden definir presupuestos para categorías de gasto");
        }

        var existenteResultado = await _presupuestoPersistencia.ObtenerPorCategoriaYMesAsync(categoriaId, anio, mes);

        if (existenteResultado.IsFailed)
        {
            _logger.LogError("Error al obtener el presupuesto de la categoría {CategoriaId} para el mes {Mes}/{Anio}: {Errors}", categoriaId, mes, anio, existenteResultado.Errors);
            return Result.Fail("Error al obtener el presupuesto");
        }

        var presupuesto = existenteResultado.Value;

        if (presupuesto is null)
        {
            presupuesto = new Presupuesto(categoriaId, limite, anio, mes);

            var resultadoAgregar = await _presupuestoPersistencia.AgregarAsync(presupuesto);

            if (resultadoAgregar.IsFailed)
            {
                _logger.LogError("Error al crear el presupuesto de la categoría {CategoriaId}: {Errors}", categoriaId, resultadoAgregar.Errors);
                return Result.Fail("Error al crear el presupuesto");
            }
        }
        else
        {
            presupuesto.Limite = limite;

            var resultadoActualizar = await _presupuestoPersistencia.ActualizarAsync(presupuesto);

            if (resultadoActualizar.IsFailed)
            {
                _logger.LogError("Error al actualizar el presupuesto de la categoría {CategoriaId}: {Errors}", categoriaId, resultadoActualizar.Errors);
                return Result.Fail("Error al actualizar el presupuesto");
            }
        }

        return Result.Ok(presupuesto);
    }

    public async Task<Result<decimal>> ObtenerGastadoAsync(int categoriaId, int anio, int mes)
    {
        var resultado = await _movimientoPersistencia.ObtenerPorMesAsync(anio, mes);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener los movimientos para el mes {Mes}/{Anio}: {Errors}", mes, anio, resultado.Errors);
            return Result.Fail("Error al calcular lo gastado");
        }

        decimal gastado = resultado.Value
            .Where(m => m.CategoriaId == categoriaId && m.Tipo == Categoria.TipoMovimiento.Gasto)
            .Sum(m => m.Importe);

        return Result.Ok(gastado);
    }

    public async Task<Result<List<Presupuesto>>> ObtenerPorMesAsync(int anio, int mes)
    {
        var resultado = await _presupuestoPersistencia.ObtenerPorMesAsync(anio, mes);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener los presupuestos para el mes {Mes}/{Anio}: {Errors}", mes, anio, resultado.Errors);
        }

        return resultado;
    }
}
