namespace CleanZone.Pages.Areas;

public class DetailsModel : PageModel
{
    private readonly AreaRepository _areaRepository;

    public DetailsModel(AreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public Area Area { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var area = await _areaRepository.GetByIdAsync(id);
        if (area == null)
        {
            return NotFound();
        }
        else
        {
            Area = area;
        }
        return Page();
    }
}
