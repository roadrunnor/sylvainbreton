namespace api_sylvainbreton.Config
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Text.Json.Serialization;

    public static class JsonOptionsExtensions
    {
        public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return builder;
        }
    }

}
