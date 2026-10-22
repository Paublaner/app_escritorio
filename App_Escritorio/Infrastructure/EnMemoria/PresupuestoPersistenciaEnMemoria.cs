using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;

namespace App_Escritorio.Infrastructure.EnMemoria;

// TEMPORAL: se sustituirá por el adaptador de EF Core + PostgreSQL
public class PresupuestoPersistenciaEnMemoria : IPresupuestoPersistencia
{
    private readonly List<Presupuesto> _presupuestos = [];
    private int _siguienteId = 1;

    public Task<Result<List<Presupuesto>>> ObtenerPorMesAsync(int anio, int mes)
    {
        return Task.FromResult(Result.Ok(_presupuestos.Where(p => p.Anio == anio && p.Mes == mes).ToList()));
    }

    public Task<Result<Presupuesto?>> ObtenerPorCategoriaYMesAsync(int categoriaId, int anio, int mes)
    {
        var presupuesto = _presupuestos.FirstOrDefault(p => p.CategoriaId == categoriaId && p.Anio == anio && p.Mes == mes);
        return Task.FromResult(Result.Ok(presupuesto));
    }

    public Task<Result> AgregarAsync(Presupuesto presupuesto)
    {
        GeneradorId.Asignar(presupuesto, _siguienteId++);
        _presupuestos.Add(presupuesto);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> ActualizarAsync(Presupuesto presupuesto)
    {
        return Task.FromResult(_presupuestos.Contains(presupuesto) ? Result.Ok() : Result.Fail($"No existe el presupuesto {presupuesto.Id}"));
    }
}
