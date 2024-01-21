using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configuration de CORS
if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("DockerDevelopment"))
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
        });
    });
}

// Configuration du contexte de base de données
var connectionString = builder.Configuration.GetConnectionString("SylvainBretonConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La chaîne de connexion 'SylvainBretonConnection' n'est pas définie.");
}

builder.Services.AddDbContext<SylvainBretonDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Maximum number of retries
            maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
            errorNumbersToAdd: null))); // SQL error numbers for additional retries (optional)


// Configuration Swagger/OpenAPI
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
app.UseCors("AllowAllOrigins"); // Make sure this matches the policy name defined above

app.UseAuthorization();
app.MapControllers();
app.Run();
