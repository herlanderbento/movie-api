using MediatR;
using Movies.Application.Commands;
using Movies.Application.Common;
using Movies.Domain.Repository;

namespace Movies.Application.Handlers;

public class UpdateMovieHandler: IRequestHandler<UpdateMovieCommand, MovieModelOutput>
{
    
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieHandler(IMovieRepository movieRepository) 
        => _movieRepository = movieRepository;
    
    public async Task<MovieModelOutput> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _movieRepository.GetByIdAsync(request.Id);
        
        if (entity is null)
        {
            throw new ApplicationException("Movie not found");
        }
        
        entity.Update(request.Title, request.Description, request.ReleaseYear);

        var movie = await _movieRepository.UpdateAsync(entity);

        return MovieModelOutput.FromMovie(movie);

    }
}