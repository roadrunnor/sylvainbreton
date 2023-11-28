using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur
builder.Services.AddControllers();

// Configuration du contexte de base de données
builder.Services.AddDbContext<SylvainBretonDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SylvainBretonConnection"),
    new MySqlServerVersion(new Version(8, 0, 27)))); // Assurez-vous que la version MySQL est correcte

// Configuration Swagger/OpenAPI
// Ajout des autres services et configuration
builder.Services.AddControllers();
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

