using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;

namespace App_Escritorio.Infrastructure.EnMemoria;

// TEMPORAL: se sustituirá por el adaptador de EF Core + PostgreSQL
public class MovimientoPersistenciaEnMemoria : IMovimientoPersistencia
{
    private readonly List<Movimiento> _movimientos = [];
    private int _siguienteId = 1;

    public Task<Result<Movimiento>> ObtenerPorIdAsync(int id)
    {
        var movimiento = _movimientos.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(movimiento is null ? Result.Fail<Movimiento>($"No existe el movimiento {id}") : Result.Ok(movimiento));
    }

    public Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId)
    {
        return Task.FromResult(Result.Ok(_movimientos.Where(m => m.CuentaId == cuentaId).ToList()));
    }

    public Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes)
    {
        return Task.FromResult(Result.Ok(_movimientos.Where(m => m.Fecha.Year == anio && m.Fecha.Month == mes).ToList()));
    }

    public Task<Result> AgregarAsync(Movimiento movimiento)
    {
        GeneradorId.Asignar(movimiento, _siguienteId++);
        _movimientos.Add(movimiento);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> EliminarAsync(int id)
    {
        int eliminados = _movimientos.RemoveAll(m => m.Id == id);
        return Task.FromResult(eliminados > 0 ? Result.Ok() : Result.Fail($"No existe el movimiento {id}"));
    }
}
