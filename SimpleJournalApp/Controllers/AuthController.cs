using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleJournalApp.Application.Interface;
using SimpleJournalApp.Domain.Entities;
using SimpleJournalApp.Infrastructure.Service;

namespace SimpleJournalApp.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly GenerateToken _jwtService;

    public AuthController(GenerateToken jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        // 🔐 Dummy check — replace with real user lookup (DB or Identity)
        if (model.Username == "admin" && model.Password == "1234")
        {
            var token = _jwtService.GenerateTokengen(model.Username, "Admin");
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }
}
