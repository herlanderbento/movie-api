using Movies.Domain.Shared;
using Movies.Domain.Entity;
using Movies.Domain.Shared.SearchableRepository;

namespace Movies.Domain.Repository;

public interface IMovieRepository: IRepository<Movie>, ISearchableRepository<Movie>
{
    Task<IEnumerable<Movie>> GetMoviesByDirector(string director, CancellationToken cancellationToken);
}