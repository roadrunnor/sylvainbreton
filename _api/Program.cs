using api_sylvainbreton.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.WebHost.UseKestrelWithCertificate();

var app = builder.Build();
await app.Services.InitializeApplicationDataAsync();
app.UseApplicationConfigurations();
app.MapControllers();
app.Run();