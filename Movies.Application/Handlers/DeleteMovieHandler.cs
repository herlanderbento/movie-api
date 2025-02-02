using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repository;
using Movies.Application.Interfaces;

namespace Movies.Application.Handlers;

public class DeleteMovieHandler: IRequestHandler<DeleteMovieCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteMovieHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        => (_movieRepository, _unitOfWork) = (movieRepository, unitOfWork);
    
    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(request.Id, cancellationToken);
        
        await _movieRepository.Delete(movie, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}