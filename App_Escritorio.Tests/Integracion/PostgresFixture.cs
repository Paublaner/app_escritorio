using App_Escritorio.Infrastructure.Driven.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.PostgreSql;

namespace App_Escritorio.Tests.Integracion;

public class PostgresFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder("postgres:17-alpine")
    .Build();


    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        using var context = CrearContexto();

        await context.Database.MigrateAsync();
    }

    public PostgresContext CrearContexto()
    {

        var option = new DbContextOptionsBuilder<PostgresContext>()
            .UseNpgsql(_postgresContainer.GetConnectionString())
            .Options;

        return new PostgresContext(option);

    }
}
