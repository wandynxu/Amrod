using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Amrod.Application;

public static class CQRSServices
{
    public static IServiceCollection AddCQRSServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
        return services;
    }
}