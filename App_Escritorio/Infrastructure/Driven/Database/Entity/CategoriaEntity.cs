using App_Escritorio.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace App_Escritorio.Infrastructure.Driven.Database.Entity;

// No puede haber dos categorías con el mismo nombre y tipo
[Index(nameof(Nombre), nameof(Tipo), IsUnique = true)]
public class CategoriaEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Categoria.TipoMovimiento Tipo { get; set; }
}
