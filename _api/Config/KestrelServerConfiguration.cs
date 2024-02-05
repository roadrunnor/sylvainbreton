namespace api_sylvainbreton.Config
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using System;

    public static class KestrelServerConfiguration
    {
        public static void ConfigureKestrelServer(WebApplicationBuilder builder)
        {
            var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
            var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5002, listenOptions =>
                {
                    listenOptions.UseHttps(certPath, certPassword);
                });
            });
        }
    }
}
