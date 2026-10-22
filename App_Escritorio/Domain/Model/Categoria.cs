using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Model;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; private set; } = string.Empty;

    public TipoMovimiento Tipo { get; private set; }
    public enum TipoMovimiento
    {
        Ingreso,
        Gasto
    }

    public Categoria(string nombre, TipoMovimiento tipo)
    {
        Nombre = nombre;
        Tipo = tipo;
    }

}
