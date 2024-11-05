using JobPortal.Domain.Abstructs;
using JobPortal.Infrastructure.Repository;
using JobPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

app.Run();
