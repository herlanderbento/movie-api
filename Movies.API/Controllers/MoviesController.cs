using Movies.API.ApiModels.Response;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Movies.Application.Commands;
using Movies.Application.Common;
using Movies.Application.Queries;
using Movies.Domain.Shared.SearchableRepository;


namespace Movies.API.Controllers;


[ApiController]
[Route("/api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<MovieModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateMovieCommand command,
        CancellationToken cancellationToken
        )
    {
        var output = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.Id },
            new ApiResponse<MovieModelOutput>(output)
        );
    }
    

    [HttpGet]
    [ProducesResponseType(typeof(ListMoviesOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        
        var query = new IListMoviesQuery();
        if (page is not null) query.Page = page.Value;
        if (perPage is not null) query.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) query.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) query.Sort = sort;
        if (dir is not null) query.Dir = dir.Value;
        
        var output = await _mediator.Send(query, cancellationToken);
        
        return Ok(
            new ApiResponseList<MovieModelOutput>(output)
        );
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<MovieModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetMovieQuery(id), cancellationToken);
        return Ok(new ApiResponse<MovieModelOutput>(output));
    }
    
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<MovieModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateMovieCommand apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateMovieCommand(
            id,
            apiInput.Title,
            apiInput.Description,
            apiInput.ReleaseYear
        );
        var output = await _mediator.Send(input, cancellationToken);
        
        return Ok(new ApiResponse<MovieModelOutput>(output));
    }


    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMovieCommand(id), cancellationToken);
        return NoContent();
    }
}