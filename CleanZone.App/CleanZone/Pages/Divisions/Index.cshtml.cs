namespace CleanZone.Pages.Divisions;

public class IndexModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public IndexModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Division> Division { get;set; } = default!;

    public async Task OnGetAsync()
    {
        /*
        if (_context.Division != null)
        {
            Division = await _context.Division
            .Include(d => d.Area).ToListAsync();
        }
        */
        string username = User.Identity.Name;
        if (_context.Division != null)
        {
            Division = await _context.Division
            .Where(r => r.Area.Residence.User.UserName == username)
            .Include(a => a.Area.Residence)
            .ToListAsync();
        }
    }
}
