namespace CleanZone.Pages.Arvoreview;
[Authorize]
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
    public IList<Residence> Residence { get; set; } = default!;
    public IList<Division> Division { get; set; } = default!;
    public IList<Area> Area { get; set; } = default!;
    [BindProperty]
    public DateTime Date { get; set; }
    public async Task OnGetAsync()
    {
        DateTime dataAtual = _dateService.ObterDataAtual();
        ViewData["DataAtual"] = dataAtual.ToString("dd/MM/yyyy HH:mm:ss");
        Date = _dateService.ObterDataAtual();

        string username = User.Identity.Name;

        if (!string.IsNullOrEmpty(username))
        {
            Residence = await _context.Residence
                .Where(r => r.User.UserName == username)
                .Include(r => r.User)
                .ToListAsync();
        }
        if (_context.Division != null)
        {
            Division = await _context.Division
            .Where(r => r.Area.Residence.User.UserName == username)
            .Include(a => a.Area.Residence.User)
            .ToListAsync();
        }
        if (_context.Area != null)
        {
            Area = await _context.Area
            .Where(r => r.Residence.User.UserName == username)
            .Include(a => a.Residence.User)
            .ToListAsync();
        }

    }
    public IActionResult OnPost(int divisionId)
    {
        if (HttpContext.Request.Form.ContainsKey("desDate"))
        {
            _dateService.DescrementarData();

            return RedirectToPage("./Index");
        }
        if (HttpContext.Request.Form.ContainsKey("advanceDate"))
        {
            _dateService.IncrementarData();

            return RedirectToPage("./Index");
        }
        if (HttpContext.Request.Form.ContainsKey("divisionId"))
        {
            var division = _context.Division.FirstOrDefault(d => d.ID == divisionId);
            if (division != null)
            {
                division.AddCleaning(_dateService.ObterDataAtual());
                _context.SaveChanges();
            }
            return RedirectToPage("./Index");
        }

        if (HttpContext.Request.Form.ContainsKey("simulador"))
        {
            var dataAtual = _dateService.ObterDataAtual();
            var dataSimulada = Date; 

            int anos = dataSimulada.Year - dataAtual.Year;
            int meses = dataSimulada.Month - dataAtual.Month;

            int dias = (anos * 12 + meses) * 30; 

            _dateService.IncrementarDataSimulation(dias);

            return RedirectToPage("./Index");
        }
        // _dateService.IncrementarData();
        return RedirectToPage("./Index");

    }

}
