using DomainEntity = Movies.Domain.Entity;

namespace Movies.Application.Common;

public class MovieModelOutput
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Director { get; set; }
    public int ReleaseYear { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get;  set;  }

    public MovieModelOutput(
        Guid id, 
        string title, 
        string description, 
        string director, 
        int releaseYear, 
        DateTime createdAt, 
        DateTime updatedAt
        )
    {
        Id = id;
        Title = title;
        Description = description;
        Director = director;
        ReleaseYear = releaseYear;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    public static MovieModelOutput FromMovie(DomainEntity.Movie movie) => new(
        movie.Id,
        movie.Title,
        movie.Description,
        movie.Director,
        movie.ReleaseYear,
        movie.CreatedAt,
        movie.UpdatedAt
    );
}