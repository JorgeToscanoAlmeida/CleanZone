using System.Security.Claims;
using YamlDotNet.Serialization;

namespace CleanZone.Pages.ImportBuilding;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ImportRepository _importRepository;

    public IndexModel(ImportRepository importRepository)
    {
        _importRepository = importRepository;
    }
    [BindProperty]
    public IFormFile ResidencyYamlFile { get; set; }
    public IActionResult OnPost()
    {
        if (ResidencyYamlFile != null && ResidencyYamlFile.Length > 0)
        {
            _importRepository.ImportBuildings(ResidencyYamlFile, User);
            return RedirectToPage("/Index");

        }
        // Tratamento de erro, se a desserialização falhar ou não houver dados no YAML
        TempData["ErrorMessage"] = "Failed to import the divisions. Check the YAML format.";
        return Page();
    }
}

