﻿namespace CleanZone.Pages.Divisions;
public class DetailsModel : PageModel
{
    private readonly DivionRepositoy _divionRepositoy;

    public DetailsModel(DivionRepositoy divionRepositoy)
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
