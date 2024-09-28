using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnet_todo_api.src.TodoApi.Models;

namespace dotnet_todo_api.src.TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;

        // Sign-Up Action
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User created successfully!");
        }

        // Sign-In Action
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            // Generate JWT Token
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            Console.WriteLine(_configuration["Jwt:Key"]);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiresInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        // Sign-Out Action
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok("User signed out successfully!");
        }
    }
}
