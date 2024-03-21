namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseApplicationConfigurations(this WebApplication app)
        {
            var env = app.Environment;

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

            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseCors("CustomCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
