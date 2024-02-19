namespace api_sylvainbreton.Extensions
{
    using api_sylvainbreton.Exceptions;

    public static class ApplicationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Apply environment-specific exception handling
            app.UseEnvironmentSpecificExceptionHandling(env);

            // Apply global exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Other middleware registrations...
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
