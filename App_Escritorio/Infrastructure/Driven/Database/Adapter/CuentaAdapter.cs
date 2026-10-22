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

public class CuentaAdapter(PostgresContext context): ICuentaPersistencia
{

    private readonly PostgresContext _context = context;

    public async Task<Result> ActualizarAsync(Cuenta cuenta)
    {
        if (cuenta == null)
        {
            return await Task.FromResult(Result.Fail("Cuenta no válida"));
        }

        var cuentaEntity = await _context.Cuentas.FindAsync(cuenta.Id);

        if (cuentaEntity == null)
        {
            return await Task.FromResult(Result.Fail("Cuenta no encontrada"));
        }

        cuentaEntity.Nombre = cuenta.Nombre;
        cuentaEntity.Saldo = cuenta.Saldo;
        cuentaEntity.Tipo = cuenta.Tipo;

        await _context.SaveChangesAsync();

        return await Task.FromResult(Result.Ok());
    }


    public async Task<Result> AgregarAsync(Cuenta cuenta)
    {
        if (cuenta == null)
        {
            return await Task.FromResult(Result.Fail("Cuenta no válida"));
        }

        var cuentaEntity = CuentaMapper.ToEntity(cuenta);
        await _context.Cuentas.AddAsync(cuentaEntity);
        await _context.SaveChangesAsync();

        cuenta.Id = cuentaEntity.Id;

        return await Task.FromResult(Result.Ok());
    }

    public async Task<Result> EliminarAsync(int id)
    {
        if (id <= 0)
        {
            return await Task.FromResult(Result.Fail("Id no válido"));
        }

        var cuentaEntity = await _context.Cuentas.FindAsync(id);

        if (cuentaEntity == null)
        {
            return await Task.FromResult(Result.Fail("Cuenta no encontrada"));
        }


        try
        {


            _context.Cuentas.Remove(cuentaEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Result.Fail($"Error al eliminar la cuenta: {ex.Message}"));
        }

        return await Task.FromResult(Result.Ok());
    }

    public async Task<Result<Cuenta>> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
        {
            return await Task.FromResult(Result.Fail<Cuenta>("Id no válido"));
        }

        var cuentaEntity = await _context.Cuentas.FindAsync(id);

        if (cuentaEntity == null)
        {
            return await Task.FromResult(Result.Fail<Cuenta>("Cuenta no encontrada"));
        }

        var cuenta = CuentaMapper.ToDomain(cuentaEntity);

        return await Task.FromResult(Result.Ok(cuenta));

    }

    public async Task<Result<List<Cuenta>>> ObtenerTodasAsync()
    {
        var cuentasEntity = await _context.Cuentas.ToListAsync();
        var cuentas = cuentasEntity.Select(CuentaMapper.ToDomain).ToList();

        return await Task.FromResult(Result.Ok(cuentas));
    }
}
