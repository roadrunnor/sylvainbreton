using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Data;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

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
var connectionString = Environment.GetEnvironmentVariable("SYLVAINBRETON_DB_CONNECTION");

builder.Services.AddDbContext<SylvainBretonDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var certificatePassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
var certificate = new X509Certificate2(@"E:\Websites\breton\_api\Certificates\certificate.pfx", certificatePassword);


// Add external authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        // Configure Google Auth
        options.ClientId = "<Google-Client-Id>";
        options.ClientSecret = "<Google-Client-Secret>";
    })
    .AddFacebook(options =>
    {
        // Configure Facebook Auth
        options.AppId = "<Facebook-App-Id>";
        options.AppSecret = "<Facebook-App-Secret>";
    })
    .AddMicrosoftAccount(options =>
    {
        // Configure Microsoft Auth
        options.ClientId = "<Microsoft-Client-Id>";
        options.ClientSecret = "<Microsoft-Client-Secret>";
    });


builder.Services.AddIdentityServerConfiguration(certificate);
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
app.UseCors("AllowAllOrigins"); 
app.UseAuthorization();
app.MapControllers();
app.Run();
