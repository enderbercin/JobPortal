using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository.Abstructs;
using JobPortal.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.PostgreSql;
using Hangfire;

namespace JobPortal.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobPortalDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("JobPortalDb")));

            var connectionString = configuration.GetConnectionString("HangfireDbConnection");

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
