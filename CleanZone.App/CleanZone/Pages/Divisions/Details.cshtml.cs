namespace CleanZone.Pages.Divisions;

public class DetailsModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public DetailsModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

  public Division Division { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Division == null)
        {
            return NotFound();
        }

        var division = await _context.Division.FirstOrDefaultAsync(m => m.ID == id);
        if (division == null)
        {
            return NotFound();
        }
        else 
        {
            Division = division;
        }
        return Page();
    }
}
