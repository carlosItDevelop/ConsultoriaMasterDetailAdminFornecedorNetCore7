using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services, IConfiguration configuration)
        {

            // Notificações
            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
