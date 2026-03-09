using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;
using Todo_List_ASP.Repositories;
using BCrypt.Net;

namespace Todo_List_ASP.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            var existing = await _repo.GetByUsernameAsync(request.Username);

            if (existing != null)
                throw new Exception("Username already exists.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            return await _repo.AddAsync(user);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 1. Find the user
            var user = await _repo.GetByUsernameAsync(request.Username);

            // 2. Check user exists AND password matches the hash
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Invalid username or password.");

            // 3. Build the JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username
            };

        }
    }
}