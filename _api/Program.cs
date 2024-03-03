using api_sylvainbreton.Extensions;
using api_sylvainbreton.Initialisations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.WebHost.UseKestrelWithCertificate();

var app = builder.Build();
app.UseApplicationConfigurations();
await app.InitializeApplicationAsync();
app.MapControllers();
app.Run();