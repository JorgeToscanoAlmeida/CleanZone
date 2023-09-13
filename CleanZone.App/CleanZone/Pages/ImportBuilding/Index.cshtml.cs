using YamlDotNet.Serialization;

namespace CleanZone.Pages.ImportBuilding;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public IFormFile BuildingYamlFile { get; set; }
    public IActionResult OnPost()
    {
        if (BuildingYamlFile != null && BuildingYamlFile.Length > 0)
        {
            using var reader = new StreamReader(BuildingYamlFile.OpenReadStream());
            var yamlContent = reader.ReadToEnd();

            var deserializer = new DeserializerBuilder().Build();
            var building = deserializer.Deserialize<Residence>(yamlContent);

            if (building != null)
            {
                _context.Database.OpenConnection();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Residence ON");
                _context.Residence.Add(building);
                _context.SaveChanges();

                return RedirectToPage("/Index");
            }
        }
        TempData["ErrorMessage"] = "Falha ao importar o edifício. Verifique o formato YAML.";
        return Page();
    }
}

