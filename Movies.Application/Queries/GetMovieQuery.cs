using MediatR;
using Movies.Application.Common;

namespace Movies.Application.Queries;

public class GetMovieQuery: IRequest<MovieModelOutput>
{
    public Guid Id { get; set; }

    public GetMovieQuery(Guid id) => Id = id;
}