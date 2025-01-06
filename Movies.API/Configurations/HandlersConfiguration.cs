using Movies.Application.Handlers;
using Movies.Infrastructure.Data.Repositories;
using Movies.Domain.Repository;

namespace Movies.API.Configurations;

public static class HandlersConfiguration
{
    public static IServiceCollection AddHandlers(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateMovieHandler>());
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IMovieRepository, MovieRepository>();
        return services;
    }
}