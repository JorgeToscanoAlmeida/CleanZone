using CleanZone.Services.Models;

namespace CleanZone.Pages.ListDivisions;
[Authorize]
public class IndexModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<DivisionViewModel> Divisions { get; set; }
    public async Task OnGetAsync()
    {
        var user = User.Identity.Name;

        if (user != null)
        {
            Divisions = _context.Division
                .Where(d => d.Area.Residence.User.UserName == user)
                .Select(d => new DivisionViewModel
                {
                    Id = d.ID,
                    Name = d.Name,
                    IsClean = d.IsClean
                    // Preencha outras propriedades relevantes do ViewModel
                })
                .ToList();


        }
    }
    public void OnPost()
    {
        if (HttpContext.Request.Form.ContainsKey("enviar"))
        {
        var user = User.Identity.Name;
            Divisions = _context.Division
            .Where(d => d.Area.Residence.User.UserName == user)
            .Select(d => new DivisionViewModel
            {
                Id = d.ID,
                Name = d.Name,
                IsClean = d.IsClean
                // Preencha outras propriedades relevantes do ViewModel
        })
                .ToList();
            foreach (var division in Divisions.Where(d => d.IsClean == false))
            {
                var emaillog = new EmailLog
                {
                    DivisionID = division.Id.ToString(),
                    EmailContent = division.Name,
                    SentAt = DateTime.Now
                };
                _context.EmailLogs.Add(emaillog);

            }

            _context.SaveChanges();
        }
    }
}

