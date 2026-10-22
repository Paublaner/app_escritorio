using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Model;

public class Presupuesto
{
    public int Id { get; set; }
    public int CategoriaId { get; private set; }
    public decimal Limite { get; set; }
    public int Anio { get; private set; }
    public int Mes { get; private set; }

    public Presupuesto(int categoriaId, decimal limite, int anio, int mes)
    {
        CategoriaId = categoriaId;
        Limite = limite;
        Anio = anio;
        Mes = mes;
    }

}
