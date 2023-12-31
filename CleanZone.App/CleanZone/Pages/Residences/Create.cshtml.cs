﻿namespace CleanZone.Pages.Residences;
[Authorize]
public class CreateModel : PageModel
{
    private readonly ResidenceRepository _residenceRepository;
    public CreateModel(ResidenceRepository residenceRepository)
    {
        _residenceRepository = residenceRepository;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;

        ViewData["UserID"] = _residenceRepository.ViewDataByName(username);

        return Page();
    }

    [BindProperty]
    public Residence Residence { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = await _residenceRepository.ObterIdDoUsuario(User.Identity.Name);

        Residence.UserID = userId;
        await _residenceRepository.AddResidencyAsync(Residence);

        return RedirectToPage("/areas/create");
    }
}
