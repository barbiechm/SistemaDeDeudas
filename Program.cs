using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.EFCore;
using SistemaDeDeudas.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sistema de Deudas API",
        Version = "v1"
    });
});

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(connectionString))
{
    // Si la variable de entorno de Render no está definida, usa la cadena de conexión local
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // ¡Reemplaza con tu cadena local real!
    Console.WriteLine("Usando la cadena de conexión local.");
}
else
{
    Console.WriteLine($"Usando la cadena de conexión de la variable de entorno.{connectionString}");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();