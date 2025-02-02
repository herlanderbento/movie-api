

using Movies.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Movies.Domain.Shared;

namespace Movies.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(MovieDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        var aggregateRoots = _context.ChangeTracker.Entries<AggregateRoot>().Select(entry => entry.Entity);
        
        _logger.LogInformation(
            "Commit: {AggregatesCount} aggregate roots with events.",
            aggregateRoots.Count());
        
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}