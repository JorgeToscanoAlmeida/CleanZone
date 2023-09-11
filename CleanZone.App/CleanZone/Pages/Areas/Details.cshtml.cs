namespace CleanZone.Pages.Areas;

public class DetailsModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public DetailsModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

  public Area Area { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Area == null)
        {
            return NotFound();
        }

        var area = await _context.Area.FirstOrDefaultAsync(m => m.Id == id);
        if (area == null)
        {
            return NotFound();
        }
        else 
        {
            Area = area;
        }
        return Page();
    }
}
