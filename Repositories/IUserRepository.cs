using Todo_List_ASP.Models;

namespace Todo_List_ASP.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> AddAsync(User user);
    }
}
