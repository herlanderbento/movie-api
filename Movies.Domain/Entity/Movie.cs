using Movies.Domain.Shared;
using Movies.Domain.Validation;

namespace Movies.Domain.Entity;

public class Movie : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Director { get; private set; }
    public int ReleaseYear { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Movie(string title, string description, string director, int releaseYear) : base()
    {
        Title = title;
        Description = description;
        Director = director;
        ReleaseYear = releaseYear;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
        Validate();
    }
    
    
    public void Update(string? title, string? description, int? releaseYear)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        ReleaseYear = releaseYear ?? ReleaseYear;
        
        UpdatedAt = DateTime.UtcNow;
        
        Validate();
    }
    
    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Title, nameof(Title));
        DomainValidation.MinLength(Title, 3, nameof(Title));
        DomainValidation.MaxLength(Title, 255, nameof(Title));
        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 4_000, nameof(Description));
    }
}