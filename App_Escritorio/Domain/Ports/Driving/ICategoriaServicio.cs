using System;
using System.Collections.Generic;
using System.Text;
using App_Escritorio.Domain.Model;
using FluentResults;

namespace App_Escritorio.Domain.Ports.Driving;

public interface ICategoriaServicio
{
    Task<Result<Categoria>> CrearAsync(string nombre, Categoria.TipoMovimiento tipo);
    Task<Result<List<Categoria>>> ObtenerTodasAsync();
    Task<Result<List<Categoria>>> ObtenerPorTipoAsync(Categoria.TipoMovimiento tipo);
    Task<Result> EliminarAsync(int id);
}
