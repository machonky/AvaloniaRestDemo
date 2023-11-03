namespace CORSDemo.Extensions;


public static class DevelopmentCorsOptions
{
    const string PolicyName = "DevelopmentPolicy";

    public static IServiceCollection AddDevelopmentCorsOptions(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName,
                builder =>
                {
                    builder
                        //.WithOrigins("https://localhost:7239", "http://localhost:5290")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseDevelopmentCors(this IApplicationBuilder app)
    {
        app.UseCors(PolicyName);
        return app;
    }
}
