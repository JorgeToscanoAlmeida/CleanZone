namespace CleanZone.Pages.Divisions;

public class CreateModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public CreateModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;
        var userResidences = _context.Area
        .Where(r => r.Residence.User.UserName == username)
.       ToList();
        ViewData["AreaID"] = new SelectList(userResidences, "Id", "Name");
        return Page();
    }

    [BindProperty]
    public Division Division { get; set; } = default!;


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {/*
      if (!ModelState.IsValid || _context.Division == null || Division == null)
        {
            return Page();
        }
      */
        _context.Division.Add(Division);
        await _context.SaveChangesAsync();
        var clean = new CleanLog()
        {
            DateTime = Division.LastClean,
            DivisionId = Division.ID,
        };
        _context.CleanLogs.Add(clean);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
