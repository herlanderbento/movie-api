using Movies.Domain.Shared;
using Movies.Domain.Entity;

namespace Movies.Domain.Repository;

public interface IMovieRepository: IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetMoviesByDirector(string director);
}