using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.EFCore;
using SistemaDeDeudas.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT");
    if (!string.IsNullOrEmpty(port))
    {
        options.ListenAnyIP(Int32.Parse(port));
    }
});
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
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
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

app.UseCors();

app.UseHttpsRedirection(); // Aseg�rate de tener esto si est�s usando HTTPS
 
app.UseRouting();

// **APLICA CORS AQU�:**

app.UseAuthorization();

app.MapControllers();

app.Run();