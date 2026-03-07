using Microsoft.EntityFrameworkCore;
using Todo_List_ASP.Data;
using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;

namespace Todo_List_ASP.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            // Check if username already exists
            var existingUser = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}