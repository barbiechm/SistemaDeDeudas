using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.EFCore;
using SistemaDeDeudas.Services;

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Reemplaza con el origen de tu frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });

    // Puedes agregar más políticas CORS aquí si tienes otros frontends
});

// **ELIMINA ESTA LÍNEA:**
// var connectionString = builder.Configuration.GetConnectionString("PostgresSQLConnection");

// Registra el AppDbContext SIN configurar la cadena de conexión aquí.
// La configuración se manejará en AppDbContext.cs
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la política CORS "AllowLocalhost"
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();