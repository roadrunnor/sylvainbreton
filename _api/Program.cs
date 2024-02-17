using api_sylvainbreton.Configurations;
using api_sylvainbreton.Data;
using api_sylvainbreton.Extensions;
using api_sylvainbreton.Services;

var builder = WebApplication.CreateBuilder(args);

// Call the extension Kestrel method with a certificate for HTTPS.
builder.WebHost.UseKestrelWithCertificate();

// Configure services, extend IMvcBuilder
builder.Services
    .AddControllersWithViews()
    .AddCustomJsonOptions();

// Configure other services, extend IServiceCollection
builder.Services
    .AddDatabaseConfiguration(builder.Configuration)
    .AddIdentityServices()
    .AddIdentityServerWithCertificate()
    .AddExternalAuthentication(builder.Configuration)
    .AddCustomCorsPolicy()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCustomDataProtection()
    .AddApplicationServices();

var app = builder.Build();

// Apply environment-specific exception handling
app.UseEnvironmentSpecificExceptionHandling(app.Environment);

// Initialize the database with roles and users
await ApplicationDbInitializer.Initialize(app.Services);

app.SeedDatabase();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
