using CleanZone.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanZone.Pages.ListDivisions;
[Authorize]
public class IndexModel : PageModel
{
    private readonly EmailService _emailService;
    private readonly DivisionRepositoy _divionRepositoy;

    public IndexModel(EmailService emailService, DivisionRepositoy divionRepositoy)
    {
        _emailService = emailService;
        _divionRepositoy = divionRepositoy;
    }
    [BindProperty]
    public List<DivisionViewModel> DivisionViewModel { get; set; }
    public async Task OnGetAsync()
    {
        var user = User.Identity.Name;

        if (user != null)
        {
            DivisionViewModel = await _divionRepositoy.GetListDivisionsByUsernameAsync(user);
        }
    }
    public async Task OnPost()
    {
        if (HttpContext.Request.Form.ContainsKey("enviar"))
        {

            var user = User.Identity.Name;

            DivisionViewModel = await _divionRepositoy.GetListDivisionsByUsernameAsync(user);
            var divisionsToEmail = DivisionViewModel.Where(d => !d.IsClean).ToList();
            foreach (var division in divisionsToEmail)
            {
                var emaillog = new EmailLog
                {
                    DivisionID = division.Id.ToString(),
                    EmailContent = division.Name,
                    SentAt = DateTime.Now,
                    EmailSubject = division.EmailSubject
                };
                await _divionRepositoy.AddEmailLogAsync(emaillog);

            }
            _emailService.EnviarEmails(DivisionViewModel);
        }
    }
}

