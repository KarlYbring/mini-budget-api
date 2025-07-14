using Microsoft.AspNetCore.Mvc;
using MiniBudgetApp.Models;
using MiniBudgetApp.Services;

namespace MiniBudgetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService; // Lägg till detta!

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService; // Sätt denna!
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var user = new User
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email
            };
            var result = await _userService.RegisterAsync(user, req.Password);
            if (result == null)
                return BadRequest("E-postadressen är redan registrerad.");
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _userService.LoginAsync(req.Email, req.Password);
            if (user == null)
                return Unauthorized("Fel e-post eller lösenord.");

            // Generera JWT-token
            var token = _jwtService.GenerateToken(user);

            // Returnera token till frontend
            return Ok(new { token });
        }
    }
}