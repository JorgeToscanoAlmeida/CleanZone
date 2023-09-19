namespace CleanZone.Pages.Residences;
[Authorize]
public class EditModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;

    public EditModel(ResidenceRepository residenceRepository)
    {
        _residenceRepository = residenceRepository;
    }

    [BindProperty]
    public Residence Residence { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var residence = await _residenceRepository.GetByIdAsync(id);
        if (residence == null)
        {
            return NotFound();
        }
        Residence = residence;
        ViewData["UserID"] = _residenceRepository.ViewDataByName(User.Identity.Name);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _residenceRepository.UpdateAsync(Residence);

        return RedirectToPage("./Index");
    }
}
