namespace api_sylvainbreton.Data
{
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("SYLVAINBRETON_DB_CONNECTION");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Database connection string not found in environment variables.");
            }

            services.AddDbContext<SylvainBretonDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


            return services;
        }
    }

}
