using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driven;

public interface IPresupuestoPersistencia
{
    Task<Result<List<Presupuesto>>> ObtenerPorMesAsync(int anio, int mes);
    Task<Result<Presupuesto?>> ObtenerPorCategoriaYMesAsync(int categoriaId, int anio, int mes);
    Task<Result> AgregarAsync(Presupuesto presupuesto);
    Task<Result> ActualizarAsync(Presupuesto presupuesto);
}
