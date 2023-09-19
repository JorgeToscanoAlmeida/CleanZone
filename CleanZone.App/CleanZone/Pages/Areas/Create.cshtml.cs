namespace CleanZone.Pages.Areas;
[Authorize]
public class CreateModel : PageModel
{
    private readonly AreaRepository _areaRepository;

    public CreateModel(AreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;
        ViewData["ResidenceID"] = _areaRepository.ViewDataByName(username);
        return Page();
    }

    [BindProperty]
    public Area Area { get; set; } = default!;
    [BindProperty]
    public List<Area> Areas { get; set; } = new List<Area>();

    [BindProperty]
    public int NumberOfAreas { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        await _areaRepository.AddAreaAsync(Area);
        return RedirectToPage("/divisions/create");

    }
}
