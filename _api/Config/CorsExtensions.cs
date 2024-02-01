﻿namespace api_sylvainbreton.Config
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                });
            });

            return services;
        }
    }

}
