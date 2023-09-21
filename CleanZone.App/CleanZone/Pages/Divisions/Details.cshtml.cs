namespace CleanZone.Pages.Divisions;
[Authorize]
public class DetailsModel : PageModel
{
    private readonly DivisionRepositoy _divionRepositoy;

    public DetailsModel(DivisionRepositoy divionRepositoy)
    {
        _divionRepositoy = divionRepositoy;
    }

    public Division Division { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var division = await _divionRepositoy.GetByIdAsync(id);
        if (division == null)
        {
            return NotFound();
        }
        else
        {
            Division = division;
        }
        return Page();
    }
}
