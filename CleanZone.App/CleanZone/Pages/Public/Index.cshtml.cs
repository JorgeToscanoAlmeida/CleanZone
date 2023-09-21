namespace CleanZone.Pages.Public;

public class IndexModel : PageModel
{
    private readonly StatisticsViewModelRepository _statisticsViewModelRepository;

    public IndexModel(StatisticsViewModelRepository statisticsViewModelRepository)
    {
        _statisticsViewModelRepository = statisticsViewModelRepository;
    }

    public StatisticsViewModel Model { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Model = await _statisticsViewModelRepository.GetModelAsync();
    }

}

