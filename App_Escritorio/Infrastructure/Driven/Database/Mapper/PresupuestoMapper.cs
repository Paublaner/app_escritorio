using App_Escritorio.Domain.Model;
using App_Escritorio.Infrastructure.Driven.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Infrastructure.Driven.Database.Mapper;

public class PresupuestoMapper
{

    public static PresupuestoEntity ToEntity(Presupuesto presupuesto)
    {
        return new PresupuestoEntity
        {
            Id = presupuesto.Id,
            CategoriaId = presupuesto.CategoriaId,
            Limite = presupuesto.Limite,
            Anio = presupuesto.Anio,
            Mes = presupuesto.Mes
        };
    }

    public static Presupuesto ToDomain(PresupuestoEntity presupuestoEntity)
    {
        Presupuesto presupuesto = new Presupuesto(presupuestoEntity.CategoriaId, presupuestoEntity.Limite,
            presupuestoEntity.Anio, presupuestoEntity.Mes);
        presupuesto.Id = presupuestoEntity.Id;

        return presupuesto;
    }

}
