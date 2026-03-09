using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;
using Todo_List_ASP.Repositories;

namespace Todo_List_ASP.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
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
                Password = request.Password
            };

            return await _repo.AddAsync(user);
        }
    }
}