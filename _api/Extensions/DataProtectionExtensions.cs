namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.DataProtection;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataProtectionExtensions
    {
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services)
        {
            var keysFolder = Environment.GetEnvironmentVariable("DP_KEYS_FOLDER"); // Ensure this environment variable is set
            var certificateThumbprint = Environment.GetEnvironmentVariable("DP_CERT_THUMBPRINT"); // Ensure this environment variable is set

            services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .ProtectKeysWithCertificate(certificateThumbprint);

            return services;
        }
    }
}
