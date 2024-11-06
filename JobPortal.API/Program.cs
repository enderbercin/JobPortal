using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using JobPortal.Application;
using Hangfire;
using JobPortal.Application.Services;

var builder = WebApplication.CreateBuilder(args);

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.{env}.json", optional: true)
 .AddEnvironmentVariables()
 .Build();

// Gerekli servisleri ekleyin
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Job Posting API", Version = "v1" });
});

//builder.Services.AddDbContext<JobPortalDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("JobPortalDb")));

// Gerekli servisler ve bağımlılıklar
builder.Services.AddApplicationLayer(configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.WebHost.UseUrls("http://*:5001");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<JobPortalDbContext>();
        context.Database.Migrate(); // Migrations ve update
        DummyData.Initialize(services); // fake data
    }
    catch (Exception ex)
    {
    }
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard("/hangfire-dashboard"); 
app.UseHangfireServer();

RecurringJob.AddOrUpdate<JobSyncService>(
    "sync-jobs-to-elasticsearch",
    service => service.SyncJobsToElasticsearch(),
    Cron.MinuteInterval(2)
);
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
