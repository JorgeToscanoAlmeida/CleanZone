namespace CleanZone.Pages.Residences;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;

    public IndexModel(ResidenceRepository residenceRepository)
    {
        _residenceRepository = residenceRepository;
    }

    public IList<Residence> Residence { get; set; } = default!;

    public async Task OnGetAsync()
    {

        string username = User.Identity.Name;
        Residence = await _residenceRepository.GetByNameAsync(username);

    }
}
