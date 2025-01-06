using MediatR;
using Movies.Application.Common;
using Movies.Application.Queries;
using Movies.Domain.Repository;

public class GetMovieByDirectorHandler: IRequestHandler<GetMovieByDirectorQuery, IEnumerable<MovieModelOutput>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByDirectorHandler(IMovieRepository movieRepository) 
        => _movieRepository = movieRepository;

    public async Task<IEnumerable<MovieModelOutput>> Handle(GetMovieByDirectorQuery request,
        CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMoviesByDirector(request.Director);

        return movies.Select(movie => MovieModelOutput.FromMovie(movie));
    }

}