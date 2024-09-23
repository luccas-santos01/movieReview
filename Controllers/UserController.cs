using Microsoft.AspNetCore.Mvc;
using CineCritique.Repository;
using CineCritique.DTO;
using CineCritique.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CineCritique.Models;

namespace CineCritique.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly TokenGenerator _tokenGenerator;

    public UserController(IUserRepository repository, TokenGenerator tokenGenerator)
    {
        _repository = repository;
        _tokenGenerator = tokenGenerator;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Admin Only")]
    public IActionResult GetUsers()
    {
        return Ok(_repository.GetUsers());
    }

    [HttpPost("signup")]
    public IActionResult AddUser([FromBody] User user)
    {
        _repository.AddUser(user);
        var token = _tokenGenerator.Generate(user);
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginDTORequest loginDTO)
    {
        User? existingUser = _repository.GetUserByEmail(loginDTO.Email!);
        if (existingUser == null) return Unauthorized(new { message = "Incorrect e-mail or password" });
        if (existingUser.Password != loginDTO.Password) return Unauthorized(new { message = "Incorrect e-mail or password" });

        var token = _tokenGenerator.Generate(existingUser);
        return Ok(new { token });
    }

    [HttpPut]
    [Route("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Admin Only")]
    public IActionResult UpdateUser(User user)
    {
        return Ok(_repository.UpdateUser(user));
    }
    [HttpDelete]
    [Route("delete/{userId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Admin Only")]
    public IActionResult DeleteUser(int userId)
    {
        _repository.DeleteUser(userId);
        return NoContent();
    }
}
