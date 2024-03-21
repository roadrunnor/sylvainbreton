namespace api_sylvainbreton.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using api_sylvainbreton.Utilities;
    using api_sylvainbreton.Configuations;

    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServerWithCertificate(this IServiceCollection services)
        {
            var idsCertThumbprint = Environment.GetEnvironmentVariable("IDS_CERT_THUMBPRINT");
            if (string.IsNullOrWhiteSpace(idsCertThumbprint))
            {
                throw new InvalidOperationException("IDS_CERT_THUMBPRINT environment variable is not set.");
            }

            var idsCertificate = CertificateUtilities.FindCertificateByThumbprint(idsCertThumbprint);
            services.AddIdentityServer()
                .AddSigningCredential(idsCertificate)
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddTestUsers(IdentityServerConfig.GetUsers());

            return services;
        }
    }
}
