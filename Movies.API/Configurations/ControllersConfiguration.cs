using Microsoft.OpenApi.Models;
using Movies.API.Filters;

namespace Movies.API.Configurations;


public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(
        this IServiceCollection services
    )
    {
        services
            .AddControllers(options
                => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            );
        
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        services.AddDocumentation();
        return services;
    }

    private static IServiceCollection AddDocumentation(
        this IServiceCollection services
    )
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Movie API",
                Version = "v1"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }


    public static WebApplication UseDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
                options.RoutePrefix = string.Empty; // Carrega o Swagger na raiz
            });
        }
        return app;
    }

}
