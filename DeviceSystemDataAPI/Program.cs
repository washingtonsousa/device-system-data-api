using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Domain.Module;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureModule();

// Add services to the container.
builder.Services.AddMediatR((config) =>
{
    var assemblies = AppDomain.CurrentDomain
    .GetAssemblies()
    .Where(a => !a.IsDynamic)
    .ToArray();

    config.RegisterServicesFromAssemblies(assemblies);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
