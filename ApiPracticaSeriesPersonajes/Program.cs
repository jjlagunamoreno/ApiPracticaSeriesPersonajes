using ApiPracticaSeriesPersonajes.Data;
using ApiPracticaSeriesPersonajes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// INYECTAMOS REPOSITORIO Y CONTEXTO
builder.Services.AddTransient<RepositoryPersonajes>();
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddDbContext<SeriesContext>(options =>
    options.UseSqlServer(connectionString));

// CONTROLADORES Y DOCUMENTACIÓN OPENAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Personajes",
        Description = "Documentación OpenAPI 3.0 para la API de Series y Personajes",
        Version = "v1"
    });
});

var app = builder.Build();

// GENERA SOLO EL DOCUMENTO OPENAPI (SIN UI)
app.UseSwagger(); // Esto genera /swagger/v1/swagger.json

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();
app.MapFallbackToFile("/redoc.html");

app.Run();

