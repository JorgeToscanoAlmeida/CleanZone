namespace CleanZone.Pages.Divisions;
[Authorize]
public class CreateModel : PageModel
{
    private readonly DivisionRepositoy _divionRepositoy;

    public CreateModel(DivisionRepositoy divionRepositoy)
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
