namespace CleanZone.Pages.Areas;

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

        // Filtrar as residências pertencentes ao usuário pelo nome do usuário
        var userResidences = _context.Residence
            .Where(r => r.User.UserName == username)
            .ToList();
        ViewData["ResidenceID"] = new SelectList(userResidences, "Id", "Name");
        return Page();
    }

    [BindProperty]
    public Area Area { get; set; } = default!;
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        /*
      if (!ModelState.IsValid || _context.Area == null || Area == null)
        {
            return Page();
        }
        */
        _context.Area.Add(Area);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
