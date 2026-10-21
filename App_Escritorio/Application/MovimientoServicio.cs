using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application;

public class MovimientoServicio(IMovimientoPersistencia movimientoPersistencia, ILogger<MovimientoServicio> logger) : IMovimientoServicio
{
    private readonly IMovimientoPersistencia _movimientoPersistencia = movimientoPersistencia;
    private readonly ILogger<MovimientoServicio> _logger = logger;

    public Task<Result> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Movimiento>> RegistrarAsync(int cuentaId, int categoriaId, decimal importe, DateTime fecha, string descripcion)
    {
        throw new NotImplementedException();
    }
}
