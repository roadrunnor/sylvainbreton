namespace api_sylvainbreton.Extensions
{
    using api_sylvainbreton.Config;
    using System.Security.Cryptography.X509Certificates;

    public static class IdentityServerExtensions
    {


        public static IServiceCollection AddIdentityServerWithCertificate(this IServiceCollection services)
        {
            // Obtain the certificate path and password from configuration.
            var certificatePath = Environment.GetEnvironmentVariable("CERT_PATH");
            if (string.IsNullOrWhiteSpace(certificatePath))
            {
                throw new InvalidOperationException("Certificate path not found in configuration.");
            }

            var certificatePassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
            if (string.IsNullOrWhiteSpace(certificatePassword))
            {
                throw new InvalidOperationException("Certificate password not found in configuration.");
            }

            // Load the certificate with the given path and password.
            var certificate = new X509Certificate2(certificatePath, certificatePassword);

            // Configure IdentityServer with the loaded certificate.
            services.AddIdentityServer()
                .AddSigningCredential(certificate)
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddTestUsers(IdentityServerConfig.GetUsers()); // This should be replaced with a real user store in production.

            return services;
        }
    }

}