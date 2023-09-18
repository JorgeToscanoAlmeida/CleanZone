namespace CleanZone.Pages;

public class IndexModel : PageModel
{
    public IndexModel()
    {
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("./Index");
    }
}