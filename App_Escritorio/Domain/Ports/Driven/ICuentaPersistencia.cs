using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driven;

public interface ICuentaPersistencia
{
    Task<Result<Cuenta>> ObtenerPorIdAsync(int id);
    Task<Result<List<Cuenta>>> ObtenerTodasAsync();
    Task<Result> AgregarAsync(Cuenta cuenta);
    Task<Result> ActualizarAsync(Cuenta cuenta);
    Task<Result> EliminarAsync(int id);
}
