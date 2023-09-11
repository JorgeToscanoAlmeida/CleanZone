namespace CleanZone.Pages.Areas;

public class DeleteModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public DeleteModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Area == null)
        {
            return NotFound();
        }
        var area = await _context.Area.FindAsync(id);

        if (area != null)
        {
            Area = area;
            _context.Area.Remove(Area);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
