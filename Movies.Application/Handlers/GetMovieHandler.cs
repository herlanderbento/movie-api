using MediatR;
using Movies.Application.Common;
using Movies.Application.Queries;
using Movies.Domain.Repository;
using Movies.Application.Interfaces;

namespace Movies.Application.Handlers;

public class GetMovieHandler: IRequestHandler<GetMovieQuery, MovieModelOutput>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetMovieHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        => (_movieRepository, _unitOfWork) = (movieRepository, unitOfWork);

    public async Task<MovieModelOutput> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(request.Id, cancellationToken);
        
        await _unitOfWork.Commit(cancellationToken);

        return MovieModelOutput.FromMovie(movie);
        
    }
}