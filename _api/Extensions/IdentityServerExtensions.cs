// File: IdentityServerExtensions.cs

namespace api_sylvainbreton.Extensions
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Config;

    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServerWithCertificate(this IServiceCollection services)
        {
            var idsCertThumbprint = Environment.GetEnvironmentVariable("IDS_CERT_THUMBPRINT");
            if (string.IsNullOrWhiteSpace(idsCertThumbprint))
            {
                throw new InvalidOperationException("IDS_CERT_THUMBPRINT environment variable is not set.");
            }

            X509Certificate2 idsCertificate = FindCertificateByThumbprint(idsCertThumbprint);
            services.AddIdentityServer()
                .AddSigningCredential(idsCertificate)
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddTestUsers(IdentityServerConfig.GetUsers()); // This should be replaced with a real user store in production.

            return services;
        }

        private static X509Certificate2 FindCertificateByThumbprint(string thumbprint)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
                if (certificates.Count == 0)
                {
                    throw new InvalidOperationException($"Certificate with thumbprint {thumbprint} not found.");
                }
                return certificates[0]; // returns the first certificate found
            }
        }
    }
}
