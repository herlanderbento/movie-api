using MediatR;
using Movies.Application.Common;
using Movies.Domain.Shared.SearchableRepository;

public class IListMoviesQuery
    : PaginatedListInput, IRequest<ListMoviesOutput>
{
    public IListMoviesQuery(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir)
    { }
    
    public IListMoviesQuery() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
