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

public class MovimientoAdapter (PostgresContext context) : IMovimientoPersistencia
{

    private readonly PostgresContext _context = context;

    public async Task<Result> AgregarAsync(Movimiento movimiento)
    {
        if (movimiento == null)
        {
            return Result.Fail("Movimiento no válido");
        }

        var movimientoEntity = MovimientoMapper.ToEntity(movimiento);
        await _context.Movimientos.AddAsync(movimientoEntity);
        await _context.SaveChangesAsync();

        movimiento.Id = movimientoEntity.Id;

        return Result.Ok();
    }

    public async Task<Result> EliminarAsync(int id)
    {
        if (id <= 0)
        {
            return Result.Fail("Id no válido");
        }

        var movimientoEntity = await _context.Movimientos.FindAsync(id);

        if (movimientoEntity == null)
        {
            return Result.Fail("Movimiento no encontrado");
        }

        _context.Movimientos.Remove(movimientoEntity);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<Movimiento>> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
        {
            return Result.Fail<Movimiento>("Id no válido");
        }

        var movimientoEntity = await _context.Movimientos.FindAsync(id);

        if (movimientoEntity == null)
        {
            return Result.Fail<Movimiento>("Movimiento no encontrado");
        }

        var movimiento = MovimientoMapper.ToDomain(movimientoEntity);

        return Result.Ok(movimiento);
    }

    public async Task<Result<List<Movimiento>>> ObtenerPorCuentaAsync(int cuentaId)
    {
        var movimientosEntity = await _context.Movimientos
            .Where(m => m.CuentaId == cuentaId)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();

        var movimientos = movimientosEntity.Select(MovimientoMapper.ToDomain).ToList();

        return Result.Ok(movimientos);
    }

    public async Task<Result<List<Movimiento>>> ObtenerPorMesAsync(int anio, int mes)
    {
        var movimientosEntity = await _context.Movimientos
            .Where(m => m.Fecha.Year == anio && m.Fecha.Month == mes)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();

        var movimientos = movimientosEntity.Select(MovimientoMapper.ToDomain).ToList();

        return Result.Ok(movimientos);
    }
}
