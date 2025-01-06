using MediatR;
using Movies.Application.Common;

namespace Movies.Application.Queries;

public class GetMovieByDirectorQuery: IRequest<IEnumerable<MovieModelOutput>>
{
    public string Director { get; set; }

    public GetMovieByDirectorQuery(string director) => Director = director;
}