using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driving;

public interface IMovimientoServicio
{
    Task<Result<Movimiento>> RegistrarAsync(int cuentaId, int categoriaId, decimal importe, DateTime fecha, string descripcion);
    Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId);
    Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes);
    Task<Result> EliminarAsync(int id);
}
