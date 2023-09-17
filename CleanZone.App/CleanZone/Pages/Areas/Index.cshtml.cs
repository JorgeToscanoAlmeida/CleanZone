using Microsoft.AspNetCore.Authorization;

namespace CleanZone.Pages.Areas;
[Authorize]
public class IndexModel : PageModel
{
    private readonly AreaRepository _areaRepository;

    public IndexModel(AreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public IList<Area> Area { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Area = await _areaRepository.GetByNameAsync(User.Identity.Name);

    }
}
