using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBudgetApp.Models;
using MiniBudgetApp.Services;
using System.Security.Claims;

namespace MiniBudgetApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst("id").Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            int userId = GetUserId();
            var transactions = await _transactionService.GetAllByUserAsync(userId);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Create(Transaction transaction)
        {
            int userId = GetUserId();
            transaction.UserId = userId; // Sätt UserId från JWT
            var created = await _transactionService.CreateAsync(transaction);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetUserId();
            var success = await _transactionService.DeleteAsync(id, userId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}