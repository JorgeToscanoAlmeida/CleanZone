using CleanZone.Repositories;

namespace CleanZone.Pages.Areas;

public class DeleteModel : PageModel
{
    private readonly AreaRepository _areaRepository;

    public DeleteModel(AreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var area = await _areaRepository.FindAsync(id);
        await _areaRepository.DeleteByIdAsync(id);

        return RedirectToPage("./Index");
    }
}
