using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Model;

public class Cuenta
{
    public int Id { get; private set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Saldo { get; set; }

    public TipoCuenta Tipo { get; set; }

    public enum TipoCuenta { 
        Banco,
        Efectivo,
        Ahorro,
        Credito

    }

    public Cuenta(string nombre, decimal saldo, TipoCuenta tipo)
    {
        Nombre = nombre;
        Saldo = saldo;
        Tipo = tipo;
    }


}
