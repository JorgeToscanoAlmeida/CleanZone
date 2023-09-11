namespace CleanZone.Pages.Residences;

public class IndexModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public IndexModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Residence> Residence { get; set; } = default!;

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
    }
}
