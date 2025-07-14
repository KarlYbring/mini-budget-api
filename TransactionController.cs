using Microsoft.AspNetCore.Mvc;
using MiniBudgetApp.Models;
using MiniBudgetApp.Services;

namespace MiniBudgetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Create(Transaction transaction)
        {
            var created = await _transactionService.CreateAsync(transaction);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
