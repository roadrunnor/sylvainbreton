namespace api_sylvainbreton.Extensions
{
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Security;

    public static class CertificateValidationExtensions
    {
        public static IServiceCollection AddCertificateValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("NamedClient").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                // Adjust server certificate validation method
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                    {
                        return true; // If there are no errors, the certificate is valid.
                    }

                    if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
                    {
                        // Inspect the chain to see if we are hitting the "UNABLE_TO_VERIFY_LEAF_SIGNATURE" error
                        return chain.ChainStatus.Length == 1 &&
                               chain.ChainStatus[0].Status == X509ChainStatusFlags.UntrustedRoot;
                    }

                    // Implement additional logic to handle other SSL policy errors as necessary.
                    return false; // If there are errors other than in the certificate chain, the certificate is invalid.
                }
            });

            return services;
        }
    }
}