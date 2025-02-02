using Movies.Application.Handlers;
using Movies.Infrastructure.Data.Repositories;
using Movies.Infrastructure.Data;
using Movies.Domain.Repository;
using Movies.Application.Interfaces;

namespace Movies.API.Configurations;

public static class HandlersConfiguration
{
    public static IServiceCollection AddHandlers(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateMovieHandler).Assembly));
        services.AddRepositories();
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IMovieRepository, MovieRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}