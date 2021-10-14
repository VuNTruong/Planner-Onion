using System.Reflection;
using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Register Mediator with DI
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Register Error getter with DI
            services.AddScoped<IErrorGetter, ErrorGetter>();

            // Register Current user service with DI
            services.AddScoped<ICurrentUser, CurrentUser>();
        }
    }
}