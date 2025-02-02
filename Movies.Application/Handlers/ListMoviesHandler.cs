using MediatR;
using Movies.Domain.Repository;
using Movies.Application.Common;


public class ListMoviesHandler : IRequestHandler<IListMoviesQuery, ListMoviesOutput>
{
    private readonly IMovieRepository _movieRepository;

    public ListMoviesHandler(IMovieRepository movieRepository)
        => _movieRepository = movieRepository;

    public async Task<ListMoviesOutput> Handle(
        IListMoviesQuery request, 
        CancellationToken cancellationToken)
    {
        var searchOutput = await _movieRepository.Search(
            new(
                request.Page, 
                request.PerPage, 
                request.Search, 
                request.Sort, 
                request.Dir
            ),
            cancellationToken
        );

        return new ListMoviesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(MovieModelOutput.FromMovie)
                .ToList()
        );
    }

}