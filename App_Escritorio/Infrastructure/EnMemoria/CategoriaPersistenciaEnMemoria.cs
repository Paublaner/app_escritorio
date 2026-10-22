using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using FluentResults;

namespace App_Escritorio.Infrastructure.EnMemoria;

// TEMPORAL: se sustituirá por el adaptador de EF Core + PostgreSQL
public class CategoriaPersistenciaEnMemoria : ICategoriaPersistencia
{
    private readonly List<Categoria> _categorias = [];
    private int _siguienteId = 1;

    public CategoriaPersistenciaEnMemoria()
    {
        // Unas categorías de ejemplo para poder probar la interfaz
        AgregarAsync(new Categoria("Nómina", Categoria.TipoMovimiento.Ingreso));
        AgregarAsync(new Categoria("Comida", Categoria.TipoMovimiento.Gasto));
        AgregarAsync(new Categoria("Transporte", Categoria.TipoMovimiento.Gasto));
        AgregarAsync(new Categoria("Ocio", Categoria.TipoMovimiento.Gasto));
        AgregarAsync(new Categoria("Hogar", Categoria.TipoMovimiento.Gasto));
    }

    public Task<Result<Categoria>> ObtenerPorIdAsync(int id)
    {
        var categoria = _categorias.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(categoria is null ? Result.Fail<Categoria>($"No existe la categoría {id}") : Result.Ok(categoria));
    }

    public Task<Result<List<Categoria>>> ObtenerTodasAsync()
    {
        return Task.FromResult(Result.Ok(_categorias.ToList()));
    }

    public Task<Result> AgregarAsync(Categoria categoria)
    {
        GeneradorId.Asignar(categoria, _siguienteId++);
        _categorias.Add(categoria);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> EliminarAsync(int id)
    {
        int eliminadas = _categorias.RemoveAll(c => c.Id == id);
        return Task.FromResult(eliminadas > 0 ? Result.Ok() : Result.Fail($"No existe la categoría {id}"));
    }
}
