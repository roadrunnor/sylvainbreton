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
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseIdentityServer();
            app.UseCors("CustomCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            return app;
        }
    }

}
