namespace CleanZone.Pages.Areas;

public class EditModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public EditModel(CleanZone.Data.ApplicationDbContext context)
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

        var area =  await _context.Area.FirstOrDefaultAsync(m => m.Id == id);
        if (area == null)
        {
            return NotFound();
        }
        Area = area;
       ViewData["ResidenceID"] = new SelectList(_context.Residence, "Id", "Id");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        /*
        if (!ModelState.IsValid)
        {
            return Page();
        }
        */
        _context.Attach(Area).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AreaExists(Area.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool AreaExists(int id)
    {
      return (_context.Area?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
