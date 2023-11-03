using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AvaloniaRestDemo.Services;

public class ConfigurationServiceBuilder
{
    public IConfiguration Build()
    {
        ///XXX_ENVIRONMENT environment variable at build time Production|Development must be set

        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName().Name;

        Stream? defaultConfigStream = null;
        Stream? envConfigStream = null;

        try
        {
            defaultConfigStream = assembly.GetManifestResourceStream($"{assemblyName}.appsettings.json");
            var builder = new ConfigurationBuilder()
                .AddJsonStream(defaultConfigStream!);

            var envConfigResourceName = $"{assemblyName}.appsettings.env.json";
            if (assembly.GetManifestResourceNames().Contains(envConfigResourceName))
            {
                envConfigStream = assembly.GetManifestResourceStream(envConfigResourceName);
                builder = builder.AddJsonStream(envConfigStream!);
            }

            return builder.Build();
        }
        finally
        {
            defaultConfigStream?.Dispose();
            envConfigStream?.Dispose();
        }
    }
}

public static class ConfigurationServiceBuilderExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        var builder = new ConfigurationServiceBuilder();
        var configuration = builder.Build();
        services
            .AddSingleton(configuration);

        return services;
    }
}