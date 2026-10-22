using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;

namespace App_Escritorio.Infrastructure.EnMemoria;

// TEMPORAL: se sustituirá por el adaptador de EF Core + PostgreSQL
public class CuentaPersistenciaEnMemoria : ICuentaPersistencia
{
    private readonly List<Cuenta> _cuentas = [];
    private int _siguienteId = 1;

    public Task<Result<Cuenta>> ObtenerPorIdAsync(int id)
    {
        var cuenta = _cuentas.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(cuenta is null ? Result.Fail<Cuenta>($"No existe la cuenta {id}") : Result.Ok(cuenta));
    }

    public Task<Result<List<Cuenta>>> ObtenerTodasAsync()
    {
        return Task.FromResult(Result.Ok(_cuentas.ToList()));
    }

    public Task<Result> AgregarAsync(Cuenta cuenta)
    {
        GeneradorId.Asignar(cuenta, _siguienteId++);
        _cuentas.Add(cuenta);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> ActualizarAsync(Cuenta cuenta)
    {
        // Las entidades son las mismas instancias de la lista, ya están actualizadas
        return Task.FromResult(_cuentas.Contains(cuenta) ? Result.Ok() : Result.Fail($"No existe la cuenta {cuenta.Id}"));
    }

    public Task<Result> EliminarAsync(int id)
    {
        int eliminadas = _cuentas.RemoveAll(c => c.Id == id);
        return Task.FromResult(eliminadas > 0 ? Result.Ok() : Result.Fail($"No existe la cuenta {id}"));
    }
}
