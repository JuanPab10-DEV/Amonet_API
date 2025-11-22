using Amonet.Application;
using Amonet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Capas de aplicación e infraestructura
builder.Services.AddAplicacion();
builder.Services.AddInfraestructura();

// Agregar controladores
builder.Services.AddControllers();

// OpenAPI y endpoints API explorer (pero sin Swagger)
builder.Services.AddEndpointsApiExplorer();

// Configurar CORS
var origenes = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("PoliticaCors", politica =>
        politica.WithOrigins(origenes)
                .AllowAnyHeader()
                .AllowAnyMethod());
});

var app = builder.Build();

// Usar CORS
app.UseCors("PoliticaCors");

// No usamos HTTPS redirection en desarrollo porque a veces rompe requests desde navegador
// Si quieres lo puedes habilitar: app.UseHttpsRedirection();

// Mapear controladores de la API
app.MapControllers();

app.Run();
