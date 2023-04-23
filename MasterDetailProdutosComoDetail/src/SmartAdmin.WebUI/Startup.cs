using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Mvc.Configuration;
using Cooperchip.ITDeveloper.Mvc.Extensions.Attibutes;
using Cooperchip.ITDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cooperchip.ITDeveloper.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builer = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (env.IsProduction() || env.IsStaging() || env.IsDevelopment())
            {
                builer.AddUserSecrets<Startup>();
            }

            Configuration = builer.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));

            services.AddMvcAndRazor(); // In MvcAndRazor
            services.AddDependencyInjectConfig(Configuration); // In DependencyInjectConfig
            services.AddBoundedContext(Configuration); // BoundedContext Farmacia


            services.Configure<SmartSettings>(Configuration.GetSection(SmartSettings.SectionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);


            // Todo: Criando minha própria IOptions: StyleButtom
            services.Configure<StyleButtom>(Configuration.GetSection(StyleButtom.SectionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<StyleButtom>>().Value);

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Intel}/{action=AnalyticsDashboard}");

                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Intel}/{action=AnalyticsDashboard}");


                endpoints.MapRazorPages();
            });
        }
    }
}
