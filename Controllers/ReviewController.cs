using Microsoft.AspNetCore.Mvc;
using CineCritique.Repository;
using CineCritique.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CineCritique.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _repository;
    public ReviewController(IReviewRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetReviews()
    {
        return Ok(_repository.GetReviews());
    }

    [HttpPost]
    public IActionResult AddReview(Review review)
    {
        return Created("", _repository.AddReview(review));
    }

    [HttpPut]
    [Route("update/{reviewId}")]
    [Authorize(Policy = "AdminOrUser")]
    public IActionResult UpdateReview(int reviewId, Review review)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var roleClaim = User.FindFirstValue(ClaimTypes.Role);

        var existingReview = _repository.GetReviewById(reviewId);
        if (existingReview == null)
        {
            return NotFound();
        }

        if (roleClaim == "True" || existingReview.UserId == int.Parse(userId))
        {
            review.ReviewId = reviewId;
            return Ok(_repository.UpdateReview(review));
        }
        else
        {
            return Forbid();
        }
    }

    [HttpDelete]
    [Route("delete/{reviewId}")]
    public IActionResult DeleteReview(int reviewId)
    {
        _repository.DeleteReview(reviewId);
        return Ok();
    }
}