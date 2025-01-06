using MediatR;
using Movies.Application.Common;

namespace Movies.Application.Commands;

public class CreateMovieCommand: IRequest<MovieModelOutput>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Director { get; set; }
    public int ReleaseYear { get; set; }

    public CreateMovieCommand(
        string title, 
        string description, 
        string director, 
        int releaseYear)
    {
        Title = title;
        Description = description;
        Director = director;
        ReleaseYear = releaseYear;
    }
}