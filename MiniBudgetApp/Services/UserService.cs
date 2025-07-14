using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Lägg till denna!
using MiniBudgetApp.Data;
using MiniBudgetApp.Models;

namespace MiniBudgetApp.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly PasswordHasher<User> _hasher = new();

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> RegisterAsync(User user, string password)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                    return null;

                user.PasswordHash = _hasher.HashPassword(user, password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return null;
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}