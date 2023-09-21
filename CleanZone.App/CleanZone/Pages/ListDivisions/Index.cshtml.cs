using Microsoft.EntityFrameworkCore;

namespace CleanZone.Pages.ListDivisions;
[Authorize]
public class IndexModel : PageModel
{
    private readonly EmailService _emailService;
    private readonly DivionRepositoy _divionRepositoy;

    public IndexModel(EmailService emailService, DivionRepositoy divionRepositoy)
    {
        _emailService = emailService;
        _divionRepositoy = divionRepositoy;
    }
    [BindProperty]
    public List<DivisionViewModel> DivisionViewModel { get; set; }
    public async Task OnGetAsync()
    {
        var user = User.Identity.Name;
        var allRecords = _context.SuaTabela.ToList();

        // Remove todos os registros da lista.
        _context.SuaTabela.RemoveRange(allRecords);

        // Confirma as alterações no banco de dados.
        _context.SaveChanges();
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

