// Program.cs
using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculatorAPI.Domain.Interfaces;
using MonthlyPremiumCalculatorAPI.Infrastructure.Data;
using MonthlyPremiumCalculatorAPI.Infrastructure.Repositories;
using MonthlyPremiumCalculatorAPI.Middleware;
using MonthlyPremiumCalculatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// EF Core (SQLite)
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("PremiumCalc")));

// DI
builder.Services.AddScoped<IOccupationRepository, OccupationRepository>();
builder.Services.AddScoped<IPremiumService, PremiumService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migrate and seed
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
