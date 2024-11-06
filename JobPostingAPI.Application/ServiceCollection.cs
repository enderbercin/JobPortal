using JobPortal.Application.Services;
using JobPortal.Application.Services.Abstracts;
using JobPortal.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobPortal.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddInfrastructureLayer(configuration);  
        services.AddScoped<IJobQualityService , JobQualityService>();
        services.AddScoped<ICompanySyncService, CompanySyncService>();
        return services;

    }
}
