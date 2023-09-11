namespace CleanZone.Pages.Residences;

public class DeleteModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public DeleteModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Residence == null)
        {
            return NotFound();
        }
        var residence = await _context.Residence.FindAsync(id);

        if (residence != null)
        {
            Residence = residence;
            _context.Residence.Remove(Residence);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
