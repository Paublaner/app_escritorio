using App_Escritorio.Application;
using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using App_Escritorio.Infrastructure.EnMemoria;
using App_Escritorio.UI;
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

            services.AddLogging(builder => builder.AddDebug());

            // Persistencia (adaptadores driven). TEMPORAL en memoria: se cambiará por EF Core + PostgreSQL
            services.AddSingleton<ICuentaPersistencia, CuentaPersistenciaEnMemoria>();
            services.AddSingleton<ICategoriaPersistencia, CategoriaPersistenciaEnMemoria>();
            services.AddSingleton<IMovimientoPersistencia, MovimientoPersistenciaEnMemoria>();
            services.AddSingleton<IPresupuestoPersistencia, PresupuestoPersistenciaEnMemoria>();

            services.AddTransient<ICuentaServicio, CuentaServicio>();
            services.AddTransient<ICategoriaServicio, CategoriaServicio>();
            services.AddTransient<IMovimientoServicio, MovimientoServicio>();
            services.AddTransient<IPresupuestoServicio, PresupuestoServicio>();

            services.AddTransient<ResumenView>();
            services.AddTransient<CuentasView>();
            services.AddTransient<MovimientosView>();
            services.AddTransient<CategoriasView>();
            services.AddTransient<PresupuestosView>();
            services.AddTransient<FormPrincipal>();

            using var provider = services.BuildServiceProvider();

            System.Windows.Forms.Application.Run(provider.GetRequiredService<FormPrincipal>());
        }
    }
}
