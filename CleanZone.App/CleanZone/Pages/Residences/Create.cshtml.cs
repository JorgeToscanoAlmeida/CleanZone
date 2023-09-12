namespace CleanZone.Pages.Residences;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;

        ViewData["UserID"] = new SelectList(_context.Users.Where(u => u.UserName == username), "Id", "UserName");
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
