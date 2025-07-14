using MiniBudgetApp.Data;
using MiniBudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniBudgetApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
