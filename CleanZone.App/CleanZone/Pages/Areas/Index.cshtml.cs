using Microsoft.AspNetCore.Authorization;

namespace CleanZone.Pages.Areas;
[Authorize]
public class IndexModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public IndexModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Area> Area { get;set; } = default!;

    public async Task OnGetAsync()
    {
        string username = User.Identity.Name;
        if (_context.Area != null)
        {
            Area = await _context.Area
            .Where(r => r.Residence.User.UserName== username)
            .Include(a => a.Residence)
            .ToListAsync();
        }
    }
}
