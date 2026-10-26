using App_Escritorio.Application;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using App_Escritorio.Infrastructure.Driven.Database.Context;
using App_Escritorio.Infrastructure.Driven.Database.Adapter;
using App_Escritorio.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App_Escritorio
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            var configuracion = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddLogging(builder => builder.AddDebug());

            services.AddHttpClient<IAsistenteIA, AsistenteAdapter>(client =>
            {
                client.BaseAddress = new Uri(configuracion["Asistente:Url"]!);
                client.Timeout = TimeSpan.FromMinutes(2); // el modelo local tarda en responder
            });


            services.AddDbContextFactory<PostgresContext>(options =>
                options.UseNpgsql(configuracion.GetConnectionString("Finanzas")));

            services.AddTransient<ICuentaPersistencia, CuentaAdapter>();
            services.AddTransient<ICategoriaPersistencia, CategoriaAdapter>();
            services.AddTransient<IMovimientoPersistencia, MovimientoAdapter>();
            services.AddTransient<IPresupuestoPersistencia, PresupuestoAdapter>();

            services.AddTransient<IAsistenteServicio, AsistenteServicio>();


            services.AddTransient<ICuentaServicio, CuentaServicio>();
            services.AddTransient<ICategoriaServicio, CategoriaServicio>();
            services.AddTransient<IMovimientoServicio, MovimientoServicio>();
            services.AddTransient<IPresupuestoServicio, PresupuestoServicio>();

            services.AddTransient<ResumenView>();
            services.AddTransient<CuentasView>();
            services.AddTransient<MovimientosView>();
            services.AddTransient<CategoriasView>();
            services.AddTransient<PresupuestosView>();
            services.AddTransient<ChatView>();
            services.AddTransient<FormPrincipal>();

            using var provider = services.BuildServiceProvider();

            System.Windows.Forms.Application.Run(provider.GetRequiredService<FormPrincipal>());
        }
    }
}
