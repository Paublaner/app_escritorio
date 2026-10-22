using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Infrastructure.Driven.Database.Context;
using App_Escritorio.Infrastructure.Driven.Database.Mapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Infrastructure.Driven.Database.Adapter;

public class CategoriaAdapter (PostgresContext context): ICategoriaPersistencia
{

    private readonly PostgresContext _context = context;

    public async Task<Result> AgregarAsync(Categoria categoria)
    {
        if (categoria == null)
        {
            return await Task.FromResult(Result.Fail("La categoría no puede ser nula."));
        }

        var categoriaEntity = CategoriaMapper.ToEntity(categoria);


        await _context.Categorias.AddAsync(categoriaEntity);
        await _context.SaveChangesAsync();

        categoria.Id = categoriaEntity.Id;

        return await Task.FromResult(Result.Ok());
    }

    public async Task<Result> EliminarAsync(int id)
    {
        if (id <= 0)
        {
            return await Task.FromResult(Result.Fail("Id no válido"));
        }

        var categoriaEntity = await _context.Categorias.FindAsync(id);

        if (categoriaEntity == null)
        {
            return await Task.FromResult(Result.Fail("Categoría no encontrada"));
        }

        _context.Categorias.Remove(categoriaEntity);

        await _context.SaveChangesAsync();

        return await Task.FromResult(Result.Ok());
    }

    public async Task<Result<Categoria>> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
        {
            return await Task.FromResult(Result.Fail("Id no válido"));
        }

        var categoriaEntity = await _context.Categorias.FindAsync(id);

        if (categoriaEntity == null)
        {
            return await Task.FromResult(Result.Fail("Categoría no encontrada"));
        }

        var categoria = CategoriaMapper.ToDomain(categoriaEntity);

        return await Task.FromResult(Result.Ok(categoria));
    }

    public async Task<Result<List<Categoria>>> ObtenerTodasAsync()
    {
        var categoriaEntities = await _context.Categorias.ToListAsync();

        var categorias = categoriaEntities.Select(CategoriaMapper.ToDomain).ToList();

        return await Task.FromResult(Result.Ok(categorias));
    }

   
}
