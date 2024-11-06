using Hangfire;
using JobPortal.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobPortal.Application.Services;

namespace JobPortal.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope()) 
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<JobPortalDbContext>();
                context.Database.Migrate(); 

                DummyData.Initialize(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
                throw; 
            }
        }

    }
    public static void AddHangfireService(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire-dashboard");
        app.UseHangfireServer();

        RecurringJob.AddOrUpdate<JobSyncService>(
            "sync-jobs-to-elasticsearch",
            service => service.SyncJobsToElasticsearch(),
            Cron.MinuteInterval(2)
        );

    }
}
