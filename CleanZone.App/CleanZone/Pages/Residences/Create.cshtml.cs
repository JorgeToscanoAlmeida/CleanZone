namespace CleanZone.Pages.Residences;

public class CreateModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;

    public CreateModel(ResidenceRepository residenceRepository)
    {
        _residenceRepository = residenceRepository;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;

        ViewData["UserID"] = _residenceRepository.ViewDataByName(username);

        return Page();
    }

    [BindProperty]
    public Residence Residence { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        await _residenceRepository.AddResidencyAsync(Residence);
        return RedirectToPage("/areas/create");
    }
}
