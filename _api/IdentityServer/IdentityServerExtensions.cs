using api_sylvainbreton.IdentityServer;
using System.Security.Cryptography.X509Certificates;

public static class IdentityServerExtensions
{
    public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, X509Certificate2 certificate)
    {
        services.AddIdentityServer()
            .AddInMemoryClients(Config.GetClients())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddTestUsers(Config.GetUsers()) // This is for demo purposes
            .AddSigningCredential(certificate); // Use the provided certificate

        return services;
    }
}
