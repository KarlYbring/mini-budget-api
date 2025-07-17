using Microsoft.AspNetCore.Mvc;
using MiniBudgetApp.Data;
using MiniBudgetApp.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly DataContext _context;
    public CategoryController(DataContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<Category>> Create(Category category)
    {

        if(!System.Text.RegularExpressions.Regex.IsMatch(category.Name, @"^[a-zA-Z0-9\s]+$"))
        {
            return BadRequest("Category name can only contain alphanumeric characters and spaces.");
        }

        var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());

        if (exists)
        {
            return BadRequest("Category already exists.");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        return Ok(await _context.Categories.ToListAsync());
    }
}