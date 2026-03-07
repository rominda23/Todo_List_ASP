using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;

namespace Todo_List_ASP.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserRequest request);
    }
}