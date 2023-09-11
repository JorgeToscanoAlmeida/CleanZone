namespace CleanZone.Pages.Residences;

public class DetailsModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public DetailsModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

  public Residence Residence { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Residence == null)
        {
            return NotFound();
        }

        var residence = await _context.Residence.FirstOrDefaultAsync(m => m.Id == id);
        if (residence == null)
        {
            return NotFound();
        }
        else 
        {
            Residence = residence;
        }
        return Page();
    }
}
