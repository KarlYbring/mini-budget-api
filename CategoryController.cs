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
