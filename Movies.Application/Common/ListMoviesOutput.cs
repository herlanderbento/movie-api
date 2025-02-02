

using Movies.Application.Common;

public class ListMoviesOutput
    : PaginatedListOutput<MovieModelOutput>
{
    public ListMoviesOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<MovieModelOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}

