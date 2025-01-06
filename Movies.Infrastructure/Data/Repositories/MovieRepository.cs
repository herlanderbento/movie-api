using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Repository;

namespace Movies.Infrastructure.Data.Repositories;

public class MovieRepository: IMovieRepository
{
    private readonly MovieDbContext _context;
    private DbSet<Movie> _movies => _context.Set<Movie>();
    
    public MovieRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> InsertAsync(Movie aggregate)
    {
        var model = (await _movies.AddAsync(aggregate)).Entity;
        await _context.SaveChangesAsync();
        
        return model;
    }

    public async Task<Movie> GetByIdAsync(Guid id)
    {
        var model = await _movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        return model ?? throw new KeyNotFoundException("Movie not found.");
    }
    
    public async Task<IEnumerable<Movie>> GetMoviesByDirector(string director)
    {
        var models = await _movies.AsNoTracking()
            .Where(m => m.Director == director)
            .ToListAsync();
        
        return models;
    }

    public async Task<IReadOnlyList<Movie>> GetAllAsync()
    {
        var models = await _movies.AsNoTracking().ToListAsync();
        return models;
    }

    public async Task<Movie> UpdateAsync(Movie aggregate)
    {
        var model = _movies.Update(aggregate).Entity;
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteAsync(Movie aggregate)
    {
        _movies.Remove(aggregate);
        await _context.SaveChangesAsync();
    }
    
}