using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CleanZone.Pages.Public;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public StatisticsViewModel Model { get; set; }
    public void OnGet()
    {
        int numberOfUsers = _context.Users.Count();
        var averageBuildings = _context.Residence
        .Select(r => r.Id)
        .Average();
        int numberOfDivisions = _context.Division.Count();

        Model = new StatisticsViewModel
        {
            NumberOfUsers = numberOfUsers,
            AverageBuildings = averageBuildings,
            NumberOfDivisions = numberOfDivisions,
        };
    }

}

