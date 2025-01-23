using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Million.Application.Behaviour;
using System.Reflection;

namespace Million.Application.Config
{
    public static class Injection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
            return services;
        }
    }
}
