using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driven;

public interface ICategoriaPersistencia
{
    Task<Result<Categoria>> ObtenerPorIdAsync(int id);
    Task<Result<List<Categoria>>> ObtenerTodasAsync();
    Task<Result> AgregarAsync(Categoria categoria);
    Task<Result> EliminarAsync(int id);
}
