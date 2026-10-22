using App_Escritorio.Domain.Model;
using App_Escritorio.Infrastructure.Driven.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Infrastructure.Driven.Database.Mapper;

public class CuentaMapper
{

    public static CuentaEntity ToEntity(Cuenta cuenta)
    {
        return new CuentaEntity
        {
            Id = cuenta.Id,
            Nombre = cuenta.Nombre,
            Saldo = cuenta.Saldo,
            Tipo = cuenta.Tipo
        };
    }

    public static Cuenta ToDomain(CuentaEntity cuentaEntity)
    {
        Cuenta cuenta = new Cuenta(cuentaEntity.Nombre, cuentaEntity.Saldo, cuentaEntity.Tipo);
        cuenta.Id = cuentaEntity.Id;

        return cuenta;
    }

}