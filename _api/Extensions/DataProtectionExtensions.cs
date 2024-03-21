namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using api_sylvainbreton.Utilities;

    public static class DataProtectionExtensions
    {
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            var keysFolder = Environment.GetEnvironmentVariable("DP_KEYS_FOLDER");
            var certificateThumbprint = Environment.GetEnvironmentVariable("DP_CERT_THUMBPRINT");

            if (string.IsNullOrWhiteSpace(keysFolder) || string.IsNullOrWhiteSpace(certificateThumbprint))
            {
                throw new InvalidOperationException("DP_KEYS_FOLDER and DP_CERT_THUMBPRINT environment variables must be set.");
            }

            var cert = CertificateUtilities.FindCertificateByThumbprint(certificateThumbprint);
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                .ProtectKeysWithCertificate(cert);

            return services;
        }
    }
}
