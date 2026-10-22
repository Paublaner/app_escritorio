using App_Escritorio.Domain.Model;
using App_Escritorio.Infrastructure.Driven.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Infrastructure.Driven.Database.Mapper;

public class CategoriaMapper
{

    public static CategoriaEntity ToEntity(Categoria categoria)
    {
        return new CategoriaEntity
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };
    }

    public static Categoria ToDomain(CategoriaEntity categoriaEntity)
    {
        Categoria categoria = new Categoria(categoriaEntity.Nombre, categoriaEntity.Tipo);
        return categoria;
    }



}
