using MediatR;
using Movies.Application.Commands;
using Movies.Application.Common;
using Movies.Domain.Entity;
using Movies.Domain.Repository;
using Movies.Application.Interfaces;

namespace Movies.Application.Handlers;

public class CreateMovieHandler: IRequestHandler<CreateMovieCommand, MovieModelOutput>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository; 
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MovieModelOutput> Handle(
        CreateMovieCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new Movie(
            request.Title, 
            request.Description, 
            request.Director, 
            request.ReleaseYear
            );

        
        await _movieRepository.Insert(entity, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return MovieModelOutput.FromMovie(entity);
    }
}