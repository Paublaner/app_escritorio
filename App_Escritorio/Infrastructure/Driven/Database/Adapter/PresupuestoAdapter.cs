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

public class PresupuestoAdapter (PostgresContext context) : IPresupuestoPersistencia
{

    private readonly PostgresContext _context = context;

    public async Task<Result> AgregarAsync(Presupuesto presupuesto)
    {
        if (presupuesto == null)
        {
            return Result.Fail("Presupuesto no válido");
        }

        var presupuestoEntity = PresupuestoMapper.ToEntity(presupuesto);
        await _context.Presupuestos.AddAsync(presupuestoEntity);
        await _context.SaveChangesAsync();

        presupuesto.Id = presupuestoEntity.Id;

        return Result.Ok();
    }

    public async Task<Result> ActualizarAsync(Presupuesto presupuesto)
    {
        if (presupuesto == null)
        {
            return Result.Fail("Presupuesto no válido");
        }

        var presupuestoEntity = await _context.Presupuestos.FindAsync(presupuesto.Id);

        if (presupuestoEntity == null)
        {
            return Result.Fail("Presupuesto no encontrado");
        }

        // Solo el límite se puede cambiar; categoría, año y mes identifican al presupuesto
        presupuestoEntity.Limite = presupuesto.Limite;

        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<List<Presupuesto>>> ObtenerPorMesAsync(int anio, int mes)
    {
        var presupuestosEntity = await _context.Presupuestos
            .Where(p => p.Anio == anio && p.Mes == mes)
            .ToListAsync();

        var presupuestos = presupuestosEntity.Select(PresupuestoMapper.ToDomain).ToList();

        return Result.Ok(presupuestos);
    }

    public async Task<Result<Presupuesto?>> ObtenerPorCategoriaYMesAsync(int categoriaId, int anio, int mes)
    {
        var presupuestoEntity = await _context.Presupuestos
            .FirstOrDefaultAsync(p => p.CategoriaId == categoriaId && p.Anio == anio && p.Mes == mes);

        // Que no exista no es un error: el servicio decide si lo crea o lo actualiza
        if (presupuestoEntity == null)
        {
            return Result.Ok<Presupuesto?>(null);
        }

        return Result.Ok<Presupuesto?>(PresupuestoMapper.ToDomain(presupuestoEntity));
    }
}
