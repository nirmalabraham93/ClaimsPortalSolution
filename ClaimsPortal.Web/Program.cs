using ClaimsPortal.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog;
using ClaimsPortal.Data.Repositories;
using ClaimsPortal.Service.Implementations;
using ClaimsPortal.Service.Interfaces;
using ClaimsPortal.Web.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Configuration setup
var configuration = builder.Configuration;

// Serilog setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information, // Set the minimum level to log
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// Use Serilog for logging
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add AutoMapper with profiles from the current assembly
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ClaimsPortalDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("CPConnection")));

// Register repositories
builder.Services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

// Register services
builder.Services.AddScoped<IPolicyHolderService, PolicyHolderService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
