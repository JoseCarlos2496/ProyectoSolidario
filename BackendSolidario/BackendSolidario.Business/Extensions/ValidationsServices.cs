using BackendSolidario.Business.Interfaces;
using BackendSolidario.Business.Services;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BackendSolidario.Business.Extensions {
    public static class ValidationsServices {

        public static IServiceCollection AddValidations(this IServiceCollection services) {

            services.AddTransient<IBaseRepository<Cliente, int>, BaseRepository<Cliente, int>>();
            services.AddTransient<IBaseRepository<Cuenta, int>, BaseRepository<Cuenta, int>>();
            services.AddTransient<IBaseRepository<Movimiento, int>, BaseRepository<Movimiento, int>>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IClienteRepository, ClienteRepository>();

            services.AddTransient<IMovimientoService, MovimientoService>();
            services.AddTransient<IMovimientoRepository, MovimientoRepository>();

            services.AddTransient<ICuentaService, CuentaService>();
            services.AddTransient<ICuentaRepository, CuentaRepository>();

            services.AddTransient<IReporteService, ReporteService>();

            services.AddTransient<IAutenticacionService, AutenticacionService>();


            return services;
        }
    }
}
