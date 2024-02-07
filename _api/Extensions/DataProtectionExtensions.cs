namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.DataProtection;
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataProtectionExtensions
    {
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services)
        {
            var keysFolder = Environment.GetEnvironmentVariable("DP_KEYS_FOLDER");
            var certificateThumbprint = Environment.GetEnvironmentVariable("DP_CERT_THUMBPRINT");

            // Check if the environment variables are set.
            if (string.IsNullOrWhiteSpace(keysFolder) || string.IsNullOrWhiteSpace(certificateThumbprint))
            {
                throw new InvalidOperationException("The DP_KEYS_FOLDER or data protection environment variables are not set properly.");
            }

            X509Certificate2 cert = FindCertificateByThumbprint(certificateThumbprint);
            services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .ProtectKeysWithCertificate(cert);

            return services;
        }

        private static X509Certificate2 FindCertificateByThumbprint(string certificateThumbprint)
        {
            if (string.IsNullOrWhiteSpace(certificateThumbprint))
            {
                throw new ArgumentException("The DP_CERT_THUMBPRINT or certificate thumbprint cannot be empty or null.");
            }

            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, validOnly: false);
                if (certificates.Count == 0)
                {
                    throw new InvalidOperationException($"Certificate with thumbprint {certificateThumbprint} not found.");
                }
                return certificates[0]; // returns the first certificate found
            }
        }
    }
}
