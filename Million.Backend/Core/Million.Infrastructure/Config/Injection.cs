using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Million.Domain.Contracts;
using Million.Infrastructure.Repositories;
using Million.Infrastructure.Repository.MongoDB;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;

namespace Million.Infrastructure.Config
{
    public static class Injection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar MongoSettings desde appsettings.json
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

            // Registrar IMongoDatabase
            services.AddSingleton(sp =>
            {
                var mongoSettings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = new MongoClient(mongoSettings.ConnectionString);
                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            // Registrar dinámicamente todos los repositorios de clases que heredan de Entity
            RegisterRepositories(services);

            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            // Obtener todas las clases que heredan de Entity
            var entityType = typeof(Million.Domain.Features.Shared.Entities.Entity);
            var derivedTypes = Assembly.GetAssembly(entityType)?
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && entityType.IsAssignableFrom(t))
                .ToList();

            if (derivedTypes == null || !derivedTypes.Any())
                throw new InvalidOperationException("No se encontraron clases que hereden de Entity.");

            foreach (var type in derivedTypes)
            {
                var repositoryType = typeof(IGenericRepository<>).MakeGenericType(type);
                var implementationType = typeof(MongoRepository<>).MakeGenericType(type);

                services.AddScoped(repositoryType, implementationType);
            }
        }
    }
}
