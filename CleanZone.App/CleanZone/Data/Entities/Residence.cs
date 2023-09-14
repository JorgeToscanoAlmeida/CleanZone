using Microsoft.AspNetCore.Identity;

namespace CleanZone.Data.Entities;

public class Residence
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserID { get; set; }
    public IdentityUser User { get; set; }

    public List<Residence> ImportFromYaml(string yamlContent)
    {
        var importedBuildings = new List<Residence>();
        return importedBuildings;
    }
}
