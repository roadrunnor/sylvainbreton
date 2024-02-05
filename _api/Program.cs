using api_sylvainbreton.Config;
using api_sylvainbreton.Extensions;

var builder = WebApplication.CreateBuilder(args);

KestrelServerConfiguration.ConfigureKestrelServer(builder);

// Configure services, extend IMvcBuilder
builder.Services
    .AddControllersWithViews()
    .AddCustomJsonOptions();

// Configure other services, extend IServiceCollection
builder.Services
    .AddDatabaseContext()
    .AddIdentityServices()
    .AddIdentityServerWithCertificate()
    .AddExternalAuthentication(builder.Configuration)
    .AddCustomCorsPolicy()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DockerDevelopment"))
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
