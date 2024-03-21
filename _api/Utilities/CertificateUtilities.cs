namespace api_sylvainbreton.Utilities
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    public static class CertificateUtilities
    {
        public static X509Certificate2 FindCertificateByThumbprint(string thumbprint, StoreName storeName = StoreName.My, StoreLocation storeLocation = StoreLocation.LocalMachine, bool validOnly = false)
        {
            using var store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly);

            var certificate = certificates.Cast<X509Certificate2>().FirstOrDefault(cert => !validOnly || cert.HasPrivateKey);
            if (certificate == null)
            {
                throw new InvalidOperationException($"Certificate with thumbprint {thumbprint} not found or does not meet requirements.");
            }

            return certificate;
        }
    }
}
