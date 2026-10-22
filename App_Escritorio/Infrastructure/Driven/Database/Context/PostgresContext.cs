using App_Escritorio.Infrastructure.Driven.Database.Entity;
using Microsoft.EntityFrameworkCore;


namespace App_Escritorio.Infrastructure.Driven.Database.Context;

public class PostgresContext : DbContext
{

    public DbSet<CuentaEntity> Cuentas { get; set; }
    public DbSet<CategoriaEntity> Categorias { get; set; }
    public DbSet<MovimientoEntity> Movimientos { get; set; }
    public DbSet<PresupuestoEntity> Presupuestos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CuentaEntity>().ToTable("Cuentas");
        modelBuilder.Entity<CategoriaEntity>().ToTable("Categorias");
        modelBuilder.Entity<MovimientoEntity>().ToTable("Movimientos");
        modelBuilder.Entity<PresupuestoEntity>().ToTable("Presupuestos");


    }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

}
