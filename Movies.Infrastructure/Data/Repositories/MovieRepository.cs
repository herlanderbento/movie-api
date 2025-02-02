using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Repository;
using Movies.Application.Exceptions;
using Movies.Domain.Shared.SearchableRepository;

namespace Movies.Infrastructure.Data.Repositories;

public class MovieRepository: IMovieRepository
{
    private readonly MovieDbContext _context;
    private DbSet<Movie> _movies => _context.Set<Movie>();
    public MovieRepository(MovieDbContext context) =>   _context = context;

    public async Task Insert(
        Movie aggregate,
        CancellationToken cancellationToken
    ) => await _movies.AddAsync(aggregate, cancellationToken);

    public async Task<Movie> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _movies.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id, 
            cancellationToken);
        
        NotFoundException.ThrowIfNull(model, $"Movie '{id}' not found.");
        
        return model!;
    }
    
    public async Task<IEnumerable<Movie>> GetMoviesByDirector(string director, CancellationToken cancellationToken)
    {
        var models = await _movies.AsNoTracking()
            .Where(m => m.Director == director)
            .ToListAsync();
        
        return models;
    }
    
    
    public async Task<SearchOutput<Movie>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _movies.AsNoTracking();
        
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        
        if(!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Title.Contains(input.Search));
        
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        
        return new(input.Page, input.PerPage, total, items);
    }
    
    public Task Update(Movie aggregate, CancellationToken _) 
        => Task.FromResult(_movies.Update(aggregate));
    
    public Task Delete(Movie aggregate, CancellationToken _)
        => Task.FromResult(_movies.Remove(aggregate));
    
    
    private IQueryable<Movie> AddOrderToQuery(
        IQueryable<Movie> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Title)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Title)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Title)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}