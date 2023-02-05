using API.Core.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace API.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .SetIsOriginAllowedToAllowWildcardSubdomains()               
                .WithOrigins("http://localhost:8080")
                .WithOrigins("https://localhost:8080")
                .WithOrigins("http://localhost:9000")
                .WithOrigins("https://localhost:9000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Calculator API",
                Version = "v1"
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.EnableAnnotations();
        });

        return services;
    }
}
