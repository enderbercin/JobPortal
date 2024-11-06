using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using JobPortal.Application;
using Hangfire;
using JobPortal.Application.Services;
using JobPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Job Posting API", Version = "v1" });
});


builder.Services.AddApplicationLayer(configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.WebHost.UseUrls("http://*:5001");

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.InitializeDatabase();

app.AddHangfireService();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
