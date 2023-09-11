namespace CleanZone.Pages.Residences;

public class CreateModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public CreateModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
    ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    [BindProperty]
    public Residence Residence { get; set; } = default!;
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        /*
      if (!ModelState.IsValid )
        {
            return Page();
        }
        */
        _context.Residence.Add(Residence);
        await _context.SaveChangesAsync();

        return RedirectToPage("/areas/create");
    }
}
