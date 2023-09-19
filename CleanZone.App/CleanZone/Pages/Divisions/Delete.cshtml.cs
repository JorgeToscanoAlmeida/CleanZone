namespace CleanZone.Pages.Divisions;
[Authorize]
public class DeleteModel : PageModel
{
    private readonly DivionRepositoy _divionRepositoy;

    public DeleteModel(DivionRepositoy divionRepositoy)
    {
        _divionRepositoy = divionRepositoy;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var division = await _divionRepositoy.FindAsync(id);

        await _divionRepositoy.DeleteByIdAsync(id);

        return RedirectToPage("./Index");
    }
}
