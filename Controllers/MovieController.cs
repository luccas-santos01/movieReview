using Microsoft.AspNetCore.Mvc;
using CineCritique.Repository;
using CineCritique.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CineCritique.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _repository;
    public MovieController(IMovieRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        return Ok(_repository.GetMovies());
    }

    [HttpPost]
    public IActionResult AddMovie(Movie movie)
    {
        return Created("", _repository.AddMovie(movie));
    }

    [HttpPut]
    [Route("update/{movieId}")]
    [Authorize(Policy = "Admin Only")]
    public IActionResult UpdateMovie(int movieId, Movie movie)
    {
        var existingMovie = _repository.GetMovieById(movieId);
        if (existingMovie == null)
        {
            return NotFound();
        }

        movie.MovieId = movieId;
        return Ok(_repository.UpdateMovie(movie));
    }

    [HttpDelete]
    [Route("delete/{movieId}")]
    [Authorize(Policy = "Admin Only")]
    public IActionResult DeleteMovie(int movieId)
    {
        _repository.DeleteMovie(movieId);
        return Ok();
    }
}