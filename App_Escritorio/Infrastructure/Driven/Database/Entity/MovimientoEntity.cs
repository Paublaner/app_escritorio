using App_Escritorio.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Escritorio.Infrastructure.Driven.Database.Entity;

public class MovimientoEntity
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    // Restrict: no se puede borrar una cuenta que tenga movimientos
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public CuentaEntity Cuenta { get; set; } = null!;

    public int CategoriaId { get; set; }

    // Restrict: no se puede borrar una categoría que tenga movimientos
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public CategoriaEntity Categoria { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Importe { get; set; }

    public Categoria.TipoMovimiento Tipo { get; set; }

    // "date" y no "timestamp with time zone": solo interesa el día y así Npgsql no exige fechas UTC
    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }

    public string Descripcion { get; set; } = string.Empty;
}
