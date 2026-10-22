using App_Escritorio.Domain.Model;
using App_Escritorio.Infrastructure.Driven.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Infrastructure.Driven.Database.Mapper;

public class MovimientoMapper
{

    public static MovimientoEntity ToEntity(Movimiento movimiento)
    {
        return new MovimientoEntity
        {
            Id = movimiento.Id,
            CuentaId = movimiento.CuentaId,
            CategoriaId = movimiento.CategoriaId,
            Importe = movimiento.Importe,
            Tipo = movimiento.Tipo,
            Fecha = movimiento.Fecha,
            Descripcion = movimiento.Descripcion
        };
    }

    public static Movimiento ToDomain(MovimientoEntity movimientoEntity)
    {
        Movimiento movimiento = new Movimiento(movimientoEntity.CuentaId, movimientoEntity.CategoriaId, movimientoEntity.Importe,
            movimientoEntity.Tipo, movimientoEntity.Fecha, movimientoEntity.Descripcion);
        movimiento.Id = movimientoEntity.Id;

        return movimiento;
    }

}
