using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Amrod.Application.Utils;

public static class DIMediatorServices
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
        return services;
    }
}