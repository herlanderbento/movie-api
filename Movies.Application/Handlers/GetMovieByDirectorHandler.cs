using MediatR;
using Movies.Application.Common;
using Movies.Application.Queries;
using Movies.Domain.Repository;
using Movies.Application.Interfaces;

public class GetMovieByDirectorHandler: IRequestHandler<GetMovieByDirectorQuery, IEnumerable<MovieModelOutput>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetMovieByDirectorHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        => (_movieRepository, _unitOfWork) = (movieRepository, unitOfWork);

    public async Task<IEnumerable<MovieModelOutput>> Handle(GetMovieByDirectorQuery request,
        CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMoviesByDirector(request.Director, cancellationToken);
        
        await _unitOfWork.Commit(cancellationToken);

        return movies.Select(movie => MovieModelOutput.FromMovie(movie));
    }

}