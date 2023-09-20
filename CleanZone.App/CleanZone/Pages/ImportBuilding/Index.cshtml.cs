using CleanZone.Data.Entities;
using System.Security.Claims;
using System.Security.Policy;
using YamlDotNet.Serialization;

namespace CleanZone.Pages.ImportBuilding;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public IFormFile ResidencyYamlFile { get; set; }
    public IActionResult OnPost()
    {
        if (ResidencyYamlFile != null && ResidencyYamlFile.Length > 0)
        {
            using var reader = new StreamReader(ResidencyYamlFile.OpenReadStream());
            var yamlContent = reader.ReadToEnd();

            var deserializer = new DeserializerBuilder().Build();
            var division = deserializer.Deserialize<Division>(yamlContent);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _context.Division.Add(division);
            _context.Area.Add(division.Area);
            division.Area.Residence.UserID = userId;
            _context.Residence.Add(division.Area.Residence);

            _context.SaveChanges();




            return RedirectToPage("/Index");

        }
        // Tratamento de erro, se a desserialização falhar ou não houver dados no YAML
        TempData["ErrorMessage"] = "Failed to import the divisions. Check the YAML format.";
        return Page();
    }
}

