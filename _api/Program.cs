using api_sylvainbreton.Configurations;
using api_sylvainbreton.Data;
using api_sylvainbreton.Extensions;
using api_sylvainbreton.Services;
using api_sylvainbreton.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Call the extension Kestrel method with a certificate for HTTPS.
builder.WebHost.UseKestrelWithCertificate();

// Enhanced services configurations
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Enhanced app configurations 
app.UseApplicationConfigurations();

// Database initialization and seeding
await ApplicationDbInitializer.Initialize(app.Services);

app.SeedDatabase();
app.MapControllers();
app.Run();