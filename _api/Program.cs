using api_sylvainbreton.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.WebHost.UseKestrelWithCertificate();

var app = builder.Build();
app.UseApplicationConfigurations();
app.MapControllers();
app.Run();