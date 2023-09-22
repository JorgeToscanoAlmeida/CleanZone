using System.Security.Claims;
using YamlDotNet.Serialization;

namespace CleanZone.Pages.ImportBuilding;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ImportRepository _importRepository;
    private readonly ILogger<IndexModel> _logger;
    public IndexModel(ImportRepository importRepository, ILogger<IndexModel> logger)
    {
        _importRepository = importRepository;
        _logger = logger;
    }
    [BindProperty]
    public IFormFile ResidencyYamlFile { get; set; }
    public IActionResult OnPost()
    {
        if (ResidencyYamlFile != null && ResidencyYamlFile.Length > 0)
        {
            // Verifica a extensão do arquivo
            if (Path.GetExtension(ResidencyYamlFile.FileName).ToLower() == ".yaml" || Path.GetExtension(ResidencyYamlFile.FileName).ToLower() == ".yml")
            {
                try
                {
                    _importRepository.ImportBuildings(ResidencyYamlFile, User);
                    return RedirectToPage("/Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro por ter um ficheiro yaml mal formatado");
                    TempData["ErrorMessage"] = "Failed to import the divisions. Check the YAML format.";
                    return Page();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "The file is not a valid YAML file.";
                return Page();
            }
        }
        TempData["ErrorMessage"] = "No file selected for import.";
        return Page();
    }
}

