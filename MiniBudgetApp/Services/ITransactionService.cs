using MiniBudgetApp.Models;

namespace MiniBudgetApp.Services;

using MiniBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllByUserAsync(int userId);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<bool> DeleteAsync(int id, int userId);
}
