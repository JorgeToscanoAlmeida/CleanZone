namespace CleanZone.Pages.Residences;

public class DetailsModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;
    public DetailsModel(ResidenceRepository residenceRepository)
    {
        _residenceRepository = residenceRepository;
    }

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
}
