using App_Escritorio.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace App_Escritorio.Infrastructure.Driven.Database.Entity;

public class CuentaEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [Precision(18, 2)]
    public decimal Saldo { get; set; }

    public Cuenta.TipoCuenta Tipo { get; set; }
}
