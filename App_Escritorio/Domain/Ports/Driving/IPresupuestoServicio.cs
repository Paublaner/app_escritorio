using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driving;

public interface IPresupuestoServicio
{
    Task<Result<Presupuesto>> DefinirAsync(int categoriaId, decimal limite, int anio, int mes);
    Task<Result<List<Presupuesto>>> ObtenerPorMesAsync(int anio, int mes);
    Task<Result<decimal>> ObtenerGastadoAsync(int categoriaId, int anio, int mes);
}
