namespace CleanZone.Pages.Residences;

public class DeleteModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;

    public DeleteModel(ResidenceRepository residenceRepository)
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
        else
        {
            Residence = residence;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var residence = await _residenceRepository.FindAsync(id);

        await _residenceRepository.DeleteByIdAsync(id);
        return RedirectToPage("./Index");
    }
}
