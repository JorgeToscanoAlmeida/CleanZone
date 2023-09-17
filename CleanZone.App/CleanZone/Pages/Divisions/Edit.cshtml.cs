﻿namespace CleanZone.Pages.Divisions;

public class EditModel : PageModel
{
    private readonly DivionRepositoy _divionRepositoy;

    public EditModel(DivionRepositoy divionRepositoy)
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
        Division = division;
        ViewData["AreaId"] = _divionRepositoy.ViewDataByName(User.Identity.Name);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _divionRepositoy.UpdateAsync(Division);
        return RedirectToPage("./Index");
    }
}
