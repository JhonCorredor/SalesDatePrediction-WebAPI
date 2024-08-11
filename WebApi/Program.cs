using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Utilities.Implementations;
using Utilities.Implements;
using Utilities.Interfaces;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("localhost")));

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddSwaggerGen();

// Agrega los servicios desde el archivo externo
ServiceExtensions.AddCustomServices(builder.Services);

builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>(provider =>
{
    // Obtener la clave directamente del appsettings.json
    var key = builder.Configuration["JwtConfig:Key"];
    return new JwtAuthenticationService(key);
});

// Crear una instancia de AutoMapperProfiles con la instancia de IJwtAuthenticationService
var jwtAuthenticationService = builder.Services.BuildServiceProvider().GetService<IJwtAuthenticationService>();

var autoMapperProfiles = new AutoMapperProfiles(jwtAuthenticationService);

// Registrar AutoMapper con la instancia de AutoMapperProfiles
builder.Services.AddAutoMapper(_ => _.AddProfile(autoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors("_myAllowSpecificOrigins");
app.Run();