using CleanZone.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YamlDotNet.Serialization;

namespace CleanZone.Repositories;

public class ImportRepository
{
    private readonly ApplicationDbContext _context;

    public ImportRepository()
    {
    }

    public ImportRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void ImportBuildings(IFormFile ResidencyYamlFile, ClaimsPrincipal User)
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
        }
    }

}
