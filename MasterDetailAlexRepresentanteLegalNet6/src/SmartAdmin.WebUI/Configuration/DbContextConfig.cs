using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultITDeveloper")));

            services.AddDatabaseDeveloperPageExceptionFilter(); // Add 5.0

            return services;
        }
    }
}
