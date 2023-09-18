﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanZone.Pages.Divisions;

public class CreateModel : PageModel
{
    private readonly DivionRepositoy _divionRepositoy;

    public CreateModel(DivionRepositoy divionRepositoy)
    {
        _divionRepositoy = divionRepositoy;
    }

    public IActionResult OnGet()
    {
        ViewData["AreaID"] = _divionRepositoy.ViewDataByName(User.Identity.Name);
        return Page();
    }

    [BindProperty]
    public Division Division { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        var validationResult = _divionRepositoy.Validate(Division.LastClean, Division.CleanTime, Division.CleanInterval);

        if (!validationResult.isValid)
        {
            ModelState.AddModelError($"Division.{validationResult.name}", validationResult.errorMessage);
            return Page();
        }

        Division = await _divionRepositoy.AddDivisionAsync(Division);
        return RedirectToPage("./Index");
    }
}
