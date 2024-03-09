using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Services.Interfaces;
using Umami.Infrastructure.Repositories;

namespace Umami.Infrastructure.Database.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            string ConnectionStr,
            Assembly mappingAssembly)
        {
            services.AddDbContext<DBContext>(opt =>
            {
                opt.UseSqlServer(ConnectionStr, config =>
                {
                    config.MigrationsAssembly(mappingAssembly.GetName().Name);
                });

                opt.EnableDetailedErrors(true);
                opt.ConfigureWarnings(e =>
                {
                    e.Default(WarningBehavior.Log);
                });
            });

            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IUmamiPostRepository, UmamiPostRepository>();


            return services;
        }

        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            var database = app.ApplicationServices.CreateScope()
                .ServiceProvider
                .GetRequiredService<DBContext>();

            database.Database.Migrate();

            return app;
        }
    }
}
