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
        .GroupBy(r => r.UserID)
        .Select(group => new
        {
            UserID = group.Key,
            AverageResidences = group.Count()
        })
        .Average(group => group.AverageResidences);

        int numberOfDivisions = _context.Division.Count();
        int numberOfCleanDivisions = _context.Division.Count(d => d.IsClean);
        int numberOfnoCleanDivisions = _context.Division.Count(d => !d.IsClean);

        Model = new StatisticsViewModel
        {
            NumberOfUsers = numberOfUsers,
            AverageBuildings = averageBuildings,
            NumberOfDivisions = numberOfDivisions,
            NumberOfCleanDivisions = numberOfCleanDivisions,
            NumberOfNoCleanDivisions = numberOfnoCleanDivisions,
        };
    }

}

