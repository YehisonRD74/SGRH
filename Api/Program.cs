using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SGRH.Application.Contracts.Repositories.dbo;
using SGRH.Application.Contracts.Repositories.Services;
using SGRH.Application.Services;
using SGRH.Application.Validators;
using SGRH.Persistences.Context;
using SGRH.Persistences.Repositories;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SGRHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SGRHContext")));

// Agrega esto para registrar automáticamente los validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateFloorValidator>();

builder.Services.AddScoped<IFloorRepository, FloorRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IFloorService, FloorService>();
//builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
