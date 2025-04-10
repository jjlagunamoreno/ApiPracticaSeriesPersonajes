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

// CONTROLADORES Y SWAGGER
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Personajes",
        Description = "Api CRUD Personajes de Series",
        Version = "v1"
    });
});

var app = builder.Build();

// ACTIVA SWAGGER SOLO EN DESARROLLO
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Personajes");
        options.RoutePrefix = ""; // Swagger se muestra en la raíz "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
