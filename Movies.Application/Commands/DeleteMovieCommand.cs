using MediatR;

namespace Movies.Application.Commands;

public class DeleteMovieCommand: IRequest
{
    public Guid Id { get; set; }
    public DeleteMovieCommand(Guid id) 
        => Id = id;
}