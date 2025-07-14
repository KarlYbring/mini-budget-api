using MiniBudgetApp.Models;

namespace MiniBudgetApp.Services
{
    public interface IUserService
    {

        Task<User?> RegisterAsync(User user, string password);
        Task<User?> LoginAsync(string email, string password);
    }
}