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
    public IFormFile ResidencyYamlFile { get; set; }
    public IActionResult OnPost()
    {
        if (ResidencyYamlFile != null && ResidencyYamlFile.Length > 0)
        {
            using var reader = new StreamReader(ResidencyYamlFile.OpenReadStream());
            var yamlContent = reader.ReadToEnd();

            var deserializer = new DeserializerBuilder().Build();
            var residencia = deserializer.Deserialize<Residence>(yamlContent);

            if (residencia != null)
            {
                _context.Database.OpenConnection();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Residence ON");
                _context.Residence.Add(residencia);
                _context.SaveChanges();

                return RedirectToPage("/Index");
            }
        }
        TempData["ErrorMessage"] = "Failed to import the residence. Check the YAML format.";
        return Page();
    }
}

