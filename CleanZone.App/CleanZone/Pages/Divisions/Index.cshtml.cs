namespace CleanZone.Pages.Divisions;

public class IndexModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public IndexModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Division> Division { get; set; } = default!;

    public async Task OnGetAsync()
    {
        string username = User.Identity.Name;
        if (_context.Division != null)
        {
            Division = await _context.Division
                .Where(a => a.Area.Residence.User.UserName == username)
            .Include(d => d.Area).ToListAsync();
        }

    }
}
