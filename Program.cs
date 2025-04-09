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


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; // Usa una convenci�n de nombres

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") // Especifica el origen permitido
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// **ELIMINA ESTA L�NEA:**
// var connectionString = builder.Configuration.GetConnectionString("PostgresSQLConnection");

// Registra el AppDbContext SIN configurar la cadena de conexi�n aqu�.
// La configuraci�n se manejar� en AppDbContext.cs
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();



app.UseRouting();


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();