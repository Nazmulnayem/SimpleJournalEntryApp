using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleJournalApp.Application.Interface;
using SimpleJournalApp.Domain.Entities;
using SimpleJournalApp.Infrastructure.Service;

namespace SimpleJournalApp.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly GenerateToken _jwtService;

    public AuthController(UserService userService, GenerateToken jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthLoginRequest model)
    {
        var user = _userService.Authenticate(model.Username, model.Password);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateTokengen(user.Username, user.Role);
        return Ok(new { token });
    }
}
