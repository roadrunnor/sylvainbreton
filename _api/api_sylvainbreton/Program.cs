using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Models;
using api_sylvainbreton.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur
builder.Services.AddControllers();

// Configuration du contexte de base de données
builder.Services.AddDbContext<SylvainBretonDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("SylvainBretonConnection")));

// Configuration Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration du pipeline de requête HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

