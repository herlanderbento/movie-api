using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repository;
using Movies.Application.Exceptions;

namespace Movies.Application.Handlers;

public class DeleteMovieHandler: IRequestHandler<DeleteMovieCommand>
{
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieHandler(IMovieRepository movieRepository) 
        => _movieRepository = movieRepository;
    
    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id);

        if (movie is null)
        {
            NotFoundException.ThrowIfNull(movie, $"Movie '{request.Id}' not found.");
        }

        await _movieRepository.DeleteAsync(movie);
    }
}