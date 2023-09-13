namespace CleanZone.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly DateService _dateService;
    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, DateService dateService)
    {
        _logger = logger;
        _context = context;
        _dateService = dateService;
    }

    public async Task OnGetAsync()
    {
    }
    public IActionResult OnPost(int divisionId)
    {


        return RedirectToPage("./Index");
    }
}