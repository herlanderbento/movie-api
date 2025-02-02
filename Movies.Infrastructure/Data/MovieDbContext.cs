using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Infrastructure.Data.Configurations;

namespace Movies.Infrastructure.Data;

public class MovieDbContext: DbContext
{
  public DbSet<Movie> Movies => Set<Movie>();
  
  public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfiguration(new MovieConfiguration());
  }
  
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    var entries = ChangeTracker
      .Entries<Movie>()
      .Where(e => e.State == EntityState.Modified);

    foreach (var entry in entries)
    {
      entry.Property("LastUpdated").CurrentValue = DateTime.Now;
    }

    return base.SaveChangesAsync(cancellationToken);
  }
  
}