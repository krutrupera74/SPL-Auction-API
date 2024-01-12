using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEncryptDecrypt encryptDecrypt;

        public AuthController(IConfiguration configuration, IUserRepository userRepository, IEncryptDecrypt encryptDecrypt)
        {
            _configuration = configuration;
            UserRepository = userRepository;
            this.encryptDecrypt = encryptDecrypt;
        }

        public IUserRepository UserRepository { get; }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest model)
        {

            Users user = await UserRepository.GetUserDataByUsername(model.Username);
            if (user == null)
            {
                return new LoginResponse
                {
                    Authenticated = false,
                    Message = "User Not Found"
                };
            }

            // Example: Validate user credentials (replace with your actual logic)
            var UserModel = await UserRepository.AuthenticateUser(user, model.Password);
            if (UserModel != null)
            {
                // Example: Get user details (replace with your actual logic)
                var userId = UserModel.Id;
                var userRole = UserModel.Role;

                // Example: Generate JWT token
                var token = GenerateJwtToken(userId, userRole);

                return new LoginResponse
                {
                    Authenticated = true,
                    Message = "Login Successful",
                    Role = userRole,
                    Token = token,
                    Username = model.Username
                };
            }
            return new LoginResponse
            {
                Authenticated = false,
                Message = "User Not Found"
            };
        }

        private string GenerateJwtToken(Guid userId, string userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, userRole),
            // Add other claims as needed
        };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
