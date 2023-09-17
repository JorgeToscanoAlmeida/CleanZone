using Microsoft.AspNetCore.Authorization;

namespace CleanZone.Pages.Divisions;
[Authorize]
public class IndexModel : PageModel
{
    private readonly DivionRepositoy _divionRepositoy;

    public IndexModel(DivionRepositoy divionRepositoy)
    {
        _divionRepositoy = divionRepositoy;
    }

    public IList<Division> Division { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Division = await _divionRepositoy.GetByNameAsync(User.Identity.Name);
    }
}
