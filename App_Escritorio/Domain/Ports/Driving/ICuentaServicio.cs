using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driving;

public interface ICuentaServicio
{
    Task<Result<Cuenta>> CrearAsync(string nombre, decimal saldoInicial, Cuenta.TipoCuenta tipo);
    Task<Result<Cuenta>> ObtenerPorIdAsync(int id);
    Task<Result<List<Cuenta>>> ObtenerTodasAsync();
    Task<Result<decimal>> ObtenerSaldoTotalAsync();
    Task<Result> RenombrarAsync(int id, string nuevoNombre);
    Task<Result> EliminarAsync(int id);
}
