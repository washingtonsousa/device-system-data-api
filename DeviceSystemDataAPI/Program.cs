using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Infrastructure.Module;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureModule();

builder.Services.AddMediatR((config) =>
{

    config.RegisterServicesFromAssembly(typeof(GetDeviceDataQuery).Assembly);
});

builder.Services.AddHealthChecks();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
