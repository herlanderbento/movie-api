using MediatR;
using Movies.Application.Common;
using Movies.Application.Queries;
using Movies.Domain.Repository;
using Movies.Application.Exceptions;

namespace Movies.Application.Handlers;

public class GetMovieHandler: IRequestHandler<GetMovieQuery, MovieModelOutput>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieHandler(IMovieRepository movieRepository) 
        => _movieRepository = movieRepository;

    public async Task<MovieModelOutput> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id);

        if (movie is null)
        {
            NotFoundException.ThrowIfNull(movie, $"Movie '{request.Id}' not found.");
        }
        
        return MovieModelOutput.FromMovie(movie);
        
    }
}