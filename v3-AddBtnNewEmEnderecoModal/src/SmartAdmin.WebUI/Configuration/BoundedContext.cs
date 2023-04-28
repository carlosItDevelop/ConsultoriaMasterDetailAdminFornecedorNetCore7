using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Repository;
using Cooperchip.ITDeveloper.Farmacia.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class BoundedContext
    {
        public static IServiceCollection AddBoundedContext(this IServiceCollection services, IConfiguration configuration)
        {

            // +
            services.AddScoped<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IEnderecoService, EnderecoService>();

            services.AddDatabaseDeveloperPageExceptionFilter(); // Add 5.0

            return services;
        }
    }


}
