using Microsoft.EntityFrameworkCore;

namespace App_Escritorio.Infrastructure.Driven.Database.Entity;

// Un único presupuesto por categoría y mes
[Index(nameof(CategoriaId), nameof(Anio), nameof(Mes), IsUnique = true)]
public class PresupuestoEntity
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    // Restrict: no se puede borrar una categoría que tenga presupuestos
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public CategoriaEntity Categoria { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Limite { get; set; }

    public int Anio { get; set; }
    public int Mes { get; set; }
}
