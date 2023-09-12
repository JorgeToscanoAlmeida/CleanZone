namespace CleanZone.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IList<Residence> Residence { get; set; } = default!;
    public IList<Division> Division { get; set; } = default!;
    public IList<Area> Area { get; set; } = default!;

    public async Task OnGetAsync()
    {

        string username = User.Identity.Name;

        if (!string.IsNullOrEmpty(username))
        {
            Residence = await _context.Residence
                .Where(r => r.User.UserName == username) // Filtra por nome de usuário
                .Include(r => r.User)
                .ToListAsync();
        }
        if (_context.Division != null)
        {
            Division = await _context.Division
            .Where(r => r.Area.Residence.User.UserName == username)
            .Include(a => a.Area.Residence.User)
            .ToListAsync();
        }
        if (_context.Area != null)
        {
            Area = await _context.Area
            .Where(r => r.Residence.User.UserName == username )
            .Include(a => a.Residence.User)
            .ToListAsync();
        }
    }
}