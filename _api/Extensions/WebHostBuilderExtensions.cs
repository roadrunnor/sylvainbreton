namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using api_sylvainbreton.Utilities;
    using System.Security.Cryptography.X509Certificates;

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

                var serverCertificate = CertificateUtilities.FindCertificateByThumbprint(kCertThumbprint, StoreName.My, StoreLocation.LocalMachine);
                serverOptions.ConfigureHttpsDefaults(listenOptions =>
                {
                    listenOptions.ServerCertificate = serverCertificate;
                });
            });

            return builder;
        }
    }
}
