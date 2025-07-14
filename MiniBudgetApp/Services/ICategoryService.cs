using MiniBudgetApp.Models;

namespace MiniBudgetApp.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> CreateAsync(Category transaction);
    }
}