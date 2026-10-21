using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application;

public class CategoriaServicio(ICategoriaPersistencia categoriaPersistencia, ILogger<CategoriaServicio> logger) : ICategoriaServicio
{

    private readonly ICategoriaPersistencia _categoriaPersistencia = categoriaPersistencia;
    private readonly ILogger<CategoriaServicio> _logger = logger;

    public async Task<Result<Categoria>> CrearAsync(string nombre, Categoria.TipoMovimiento tipo)
    {
        if (string.IsNullOrEmpty(nombre))
        {
            _logger.LogError("El nombre de la categoría es obligatorio");
            return Result.Fail("El nombre de la categoría es obligatorio");
        }

        if (tipo != Categoria.TipoMovimiento.Ingreso && tipo != Categoria.TipoMovimiento.Gasto)
        {
            _logger.LogError("El tipo de movimiento de la categoría es obligatorio");
            return Result.Fail("El tipo de movimiento de la categoría es obligatorio");
        }

        Categoria categoria = new Categoria(nombre, tipo);

        var resultado = await _categoriaPersistencia.AgregarAsync(categoria);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al crear la categoría");
            return Result.Fail("Error al crear la categoría");
        }

        return Result.Ok(categoria);
    }

    public async Task<Result> EliminarAsync(int id)
    {
        var resultado = await _categoriaPersistencia.EliminarAsync(id);

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al eliminar la categoría");
            return Result.Fail("Error al eliminar la categoría");
        }

        return Result.Ok();
    }

    public async Task<Result<List<Categoria>>> ObtenerPorTipoAsync(Categoria.TipoMovimiento tipo)
    {
        var resultado = await _categoriaPersistencia.ObtenerTodasAsync();

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener las categorías");
            return Result.Fail("Error al obtener las categorías");
        }

        var todas = resultado.Value;

        var filtradas = todas.Where(c => c.Tipo == tipo).ToList();

        return Result.Ok(filtradas);
    }

    public async Task<Result<List<Categoria>>> ObtenerTodasAsync()
    {
        var resultado = await _categoriaPersistencia.ObtenerTodasAsync();

        if (resultado.IsFailed)
        {
            _logger.LogError("Error al obtener las categorías");
            return Result.Fail("Error al obtener las categorías");
        }

        return Result.Ok(resultado.Value);

    }
}
