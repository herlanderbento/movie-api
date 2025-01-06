using MediatR;
using Movies.Application.Commands;
using Movies.Application.Common;
using Movies.Domain.Entity;
using Movies.Domain.Repository;

namespace Movies.Application.Handlers;

public class CreateMovieHandler: IRequestHandler<CreateMovieCommand, MovieModelOutput>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieHandler(IMovieRepository movieRepository) 
        => _movieRepository = movieRepository;
    
    public async Task<MovieModelOutput> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = new Movie(
            request.Title, 
            request.Description, 
            request.Director, 
            request.ReleaseYear
            );

        var movie = await _movieRepository.InsertAsync(entity);
        
        return MovieModelOutput.FromMovie(movie);
    }
}