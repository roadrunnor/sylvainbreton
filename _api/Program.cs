using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur
builder.Services.AddControllers();

// By adding this line, you'll instruct the system to use the naming convention from your C# models
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


// Configuration de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://client:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Configuration du contexte de base de données
var connectionString = builder.Configuration.GetConnectionString("SylvainBretonConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La chaîne de connexion 'SylvainBretonConnection' n'est pas définie.");
}

builder.Services.AddDbContext<SylvainBretonDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuration Swagger/OpenAPI
// Ajout des autres services et configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration du pipeline de requête HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DockerDevelopment"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utilisation de la politique CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();
app.Run();

