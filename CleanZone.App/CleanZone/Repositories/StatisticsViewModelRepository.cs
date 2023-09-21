namespace CleanZone.Repositories;

public class StatisticsViewModelRepository
{
    private readonly ApplicationDbContext _context;
    public StatisticsViewModelRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StatisticsViewModel> GetModelAsync()
    {
        int numberOfUsers = await _context.Users.CountAsync();
        var averageBuildings = _context.Residence
        .GroupBy(r => r.UserID)
        .Select(group => new
        {
            UserID = group.Key,
            AverageResidences = group.Count()
        })
        .Average(group => group.AverageResidences);

        int numberOfDivisions = await _context.Division.CountAsync();
        int numberOfCleanDivisions = await _context.Division.CountAsync(d => d.IsClean);
        int numberOfnoCleanDivisions = await _context.Division.CountAsync(d => !d.IsClean);

        return new StatisticsViewModel
        {
            NumberOfUsers = numberOfUsers,
            AverageBuildings = averageBuildings,
            NumberOfDivisions = numberOfDivisions,
            NumberOfCleanDivisions = numberOfCleanDivisions,
            NumberOfNoCleanDivisions = numberOfnoCleanDivisions,
        };
    }
}
