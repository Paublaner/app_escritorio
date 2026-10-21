using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driven;

public interface IMovimientoPersistencia
{
    Task<Result<Movimiento>> ObtenerPorIdAsync(int id);
    Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId);
    Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes);
    Task<Result> AgregarAsync(Movimiento movimiento);
    Task<Result> EliminarAsync(int id);
}
