namespace CleanZone.Pages.ListDivisions;
[Authorize]
public class IndexModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<DivisionViewModel> Divisions { get; set; }
    public async Task OnGetAsync()
    {
        var user = User.Identity.Name;

        if (user != null)
        {
            // Consulte o banco de dados para obter as divisões do usuário logado
            Divisions = _context.Division
                .Where(d => d.Area.Residence.User.UserName == user)
                .Select(d => new DivisionViewModel
                {
                    Id = d.ID,
                    Name = d.Name,
                    IsClean = d.IsClean
                    // Preencha outras propriedades relevantes do ViewModel
                })
                .ToList();
        }
    }
}
