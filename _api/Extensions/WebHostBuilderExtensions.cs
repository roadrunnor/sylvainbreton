namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using System.Security.Cryptography.X509Certificates;
    using System;

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseKestrelWithCertificate(this IWebHostBuilder builder)
        {
            builder.ConfigureKestrel(serverOptions =>
            {
                var kCertThumbprint = Environment.GetEnvironmentVariable("K_CERT_THUMBPRINT");
                if (string.IsNullOrWhiteSpace(kCertThumbprint))
                {
                    throw new InvalidOperationException("K_CERT_THUMBPRINT environment variable is not set.");
                }

                X509Certificate2 serverCertificate = FindCertificateByThumbprint(kCertThumbprint, StoreName.My, StoreLocation.LocalMachine); // Or StoreLocation.LocalMachine

                serverOptions.ConfigureHttpsDefaults(listenOptions =>
                {
                    listenOptions.ServerCertificate = serverCertificate;
                });
            });

            return builder;
        }

        private static X509Certificate2 FindCertificateByThumbprint(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            using (var store = new X509Store(storeName, storeLocation))
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
                if (certificates.Count == 0)
                {
                    throw new InvalidOperationException($"Certificate with thumbprint {thumbprint} not found.");
                }
                return certificates[0];
            }
        }
    }
}
