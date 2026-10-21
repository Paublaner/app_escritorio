using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Model;

public class Movimiento
{
    public int Id { get; private set; }
    public int CuentaId { get; private set; }
    public int CategoriaId { get; private set; }
    public decimal Importe { get; private set; }
    public Categoria.TipoMovimiento Tipo { get; private set; }
    public DateTime Fecha { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;

    public Movimiento(int cuentaId, int categoriaId, decimal importe, Categoria.TipoMovimiento tipo, DateTime fecha, string descripcion)
    {
        CuentaId = cuentaId;
        CategoriaId = categoriaId;
        Importe = importe;
        Tipo = tipo;
        Fecha = fecha;
        Descripcion = descripcion;
    }

}
