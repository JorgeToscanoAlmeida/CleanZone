namespace CleanZone.Pages.Areas;

public class EditModel : PageModel
{
    private readonly AreaRepository _areaRepository;

    public EditModel(AreaRepository areaRepository)
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
        Area = area;
        string username = User.Identity.Name;
        ViewData["ResidenceID"] = _areaRepository.ViewDataByName(username);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _areaRepository.UpdateAsync(Area);
        return RedirectToPage("./Index");
    }
}
