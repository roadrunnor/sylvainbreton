namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEnvironmentSpecificExceptionHandling(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("DockerDevelopment"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseGlobalErrorHandling();
            }

            return app;
        }
    }

}
