namespace SL_Backend.Security
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SL_Backend.Models;
    using System.Security.Cryptography;
    using System.Text;
    using Data;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Name == request.Username))
                return BadRequest("Username already exists");

            var passwordHash = HashPassword(request.Password);

            var user = new User
            {
                Name = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Username);

            if (user == null || user.PasswordHash != HashPassword(request.Password))
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("d72ada5666d21f44848b4c342bf79e6cea9b41e19a3673988e90526eff256eb916c0e2e1ea612e9bfdec86bdd40abd30106e9ab7e9c65dab18f7445237f7d53733dc91a889d73bb0c07ae713c80df3e0213159411f20dcc5b983b981651e3c1b88c4ab70e339040b34f5e86a76b91c948be28737d4ebff0cc5444fbe591049ff43a876356268b44e18216b81a132f3028de2802cdc3ffd562bf741761a4b0c34db0d4faad63bbb17b23ec19fc4483d4e6c3a7bdc23912ea13ab0d201a69eb1b9c425ec297ce8d0d419897d5710e345bbb7a3cd6495e4e6a4c9a1e3e51aef8ae1d089e5bf0346693dcb1676240fb91eb4a8a2627a322f48c96e75a4d44d7f3979");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}
