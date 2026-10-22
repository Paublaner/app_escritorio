using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;

namespace App_Escritorio.UI;

// Construcción de filas compartida entre varias vistas
public static class Consultas
{
    public static List<FilaMovimiento> FilasMovimientos(IEnumerable<Movimiento> movimientos, List<Cuenta> cuentas, List<Categoria> categorias)
    {
        return movimientos
            .OrderByDescending(m => m.Fecha)
            .Select(m => new FilaMovimiento
            {
                Id = m.Id,
                EsGasto = m.Tipo == Categoria.TipoMovimiento.Gasto,
                Fecha = m.Fecha,
                Cuenta = cuentas.FirstOrDefault(c => c.Id == m.CuentaId)?.Nombre ?? "—",
                Categoria = categorias.FirstOrDefault(c => c.Id == m.CategoriaId)?.Nombre ?? "—",
                Importe = m.Tipo == Categoria.TipoMovimiento.Gasto ? -m.Importe : m.Importe,
                Descripcion = m.Descripcion
            })
            .ToList();
    }

    // Gastos agrupados por categoría de mayor a menor; a partir de la sexta se juntan en "Otros"
    public static List<(string Nombre, decimal Valor)> GastosPorCategoria(IEnumerable<Movimiento> movimientos, List<Categoria> categorias, int maximo = 6)
    {
        var gastos = movimientos
            .Where(m => m.Tipo == Categoria.TipoMovimiento.Gasto)
            .GroupBy(m => m.CategoriaId)
            .Select(g => (Nombre: categorias.FirstOrDefault(c => c.Id == g.Key)?.Nombre ?? "—", Valor: g.Sum(m => m.Importe)))
            .OrderByDescending(d => d.Valor)
            .ToList();

        if (gastos.Count <= maximo)
        {
            return gastos;
        }

        var principales = gastos.Take(maximo - 1).ToList();
        principales.Add(("Otros", gastos.Skip(maximo - 1).Sum(d => d.Valor)));

        return principales;
    }

    public static async Task<Result<List<FilaPresupuesto>>> FilasPresupuestosAsync(IPresupuestoServicio presupuestoServicio, List<Categoria> categorias, int anio, int mes)
    {
        var presupuestos = await presupuestoServicio.ObtenerPorMesAsync(anio, mes);

        if (presupuestos.IsFailed)
        {
            return Result.Fail(presupuestos.Errors);
        }

        var filas = new List<FilaPresupuesto>();

        foreach (var presupuesto in presupuestos.Value)
        {
            var gastado = await presupuestoServicio.ObtenerGastadoAsync(presupuesto.CategoriaId, anio, mes);

            if (gastado.IsFailed)
            {
                return Result.Fail(gastado.Errors);
            }

            filas.Add(new FilaPresupuesto
            {
                CategoriaId = presupuesto.CategoriaId,
                Categoria = categorias.FirstOrDefault(c => c.Id == presupuesto.CategoriaId)?.Nombre ?? "—",
                Limite = presupuesto.Limite,
                Gastado = gastado.Value,
                Restante = presupuesto.Limite - gastado.Value,
                Uso = presupuesto.Limite == 0 ? 0 : gastado.Value / presupuesto.Limite
            });
        }

        return Result.Ok(filas);
    }
}
