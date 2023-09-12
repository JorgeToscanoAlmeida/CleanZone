namespace CleanZone.Pages.Residences;

public class EditModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public EditModel(CleanZone.Data.ApplicationDbContext context)
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

        var residence =  await _context.Residence.FirstOrDefaultAsync(m => m.Id == id);
        if (residence == null)
        {
            return NotFound();
        }
        Residence = residence;
       ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
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
        _context.Attach(Residence).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ResidenceExists(Residence.Id))
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

    private bool ResidenceExists(int id)
    {
      return (_context.Residence?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
