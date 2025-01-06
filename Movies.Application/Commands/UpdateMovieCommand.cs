using MediatR;
using Movies.Application.Common;

namespace Movies.Application.Commands;

public class UpdateMovieCommand: IRequest<MovieModelOutput>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public int? ReleaseYear { get; set; }

    public UpdateMovieCommand(
        Guid id, 
        string? title, 
        string? description, 
        int? releaseYear)
    {
        Id = id;
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
    }
}