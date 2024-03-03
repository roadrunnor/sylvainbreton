namespace api_sylvainbreton.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using api_sylvainbreton.Exceptions;


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

            // Global exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Other middleware registrations...
            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();


            return app;
        }
    }

}
