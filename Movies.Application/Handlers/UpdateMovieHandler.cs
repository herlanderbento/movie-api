using MediatR;
using Movies.Application.Commands;
using Movies.Application.Common;
using Movies.Domain.Repository;
using Movies.Application.Exceptions;
using Movies.Application.Interfaces;


namespace Movies.Application.Handlers;

public class UpdateMovieHandler: IRequestHandler<UpdateMovieCommand, MovieModelOutput>
{
    
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovieHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository; 
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MovieModelOutput> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _movieRepository.GetById(request.Id, cancellationToken);
        
        entity.Update(request.Title, request.Description, request.ReleaseYear);

        await _movieRepository.Update(entity,cancellationToken );
        await _unitOfWork.Commit(cancellationToken);

        return MovieModelOutput.FromMovie(entity);

    }
}