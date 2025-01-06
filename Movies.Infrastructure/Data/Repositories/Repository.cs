using Microsoft.EntityFrameworkCore;
using Movies.Domain.Shared;

namespace Movies.Infrastructure.Data.Repositories;

public class Repository<TAggregate>: IRepository<TAggregate> where TAggregate: AggregateRoot
{
    private readonly MovieDbContext _movieDbContext;

    public Repository(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    public async Task<TAggregate> InsertAsync(TAggregate aggregate)
    {
        await _movieDbContext.Set<TAggregate>().AddAsync(aggregate);
        await _movieDbContext.SaveChangesAsync();
        return aggregate;
    }

    public async Task<TAggregate> GetByIdAsync(Guid id)
    {
        var model = await _movieDbContext.Set<TAggregate>().FindAsync(id);
        return model ?? Activator.CreateInstance<TAggregate>();
    }
    
    public async Task<IReadOnlyList<TAggregate>> GetAllAsync()
    {
        return await _movieDbContext.Set<TAggregate>().ToListAsync();
    }

    public async Task<TAggregate> UpdateAsync(TAggregate aggregate)
    {
        _movieDbContext.Entry(aggregate).State = EntityState.Modified;
        await _movieDbContext.SaveChangesAsync();
        return aggregate;
    }

    public async Task DeleteAsync(TAggregate aggregate)
    {
        _movieDbContext.Set<TAggregate>().Remove(aggregate);
        await _movieDbContext.SaveChangesAsync();
    }
}