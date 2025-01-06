using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers;


[ApiController]
[Route("/api/movies")]
public class MoviesController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}