using CleanZone.Repositories;
using CleanZone.Services;
using System.ComponentModel.DataAnnotations;

namespace CleanZone.Pages.Arvoreview;
[Authorize]
public class IndexModel : PageModel
{
    public readonly ApplicationDbContext _context;
    private readonly DateService _dateService;
    private readonly AreaRepository _areaRepository;
    private readonly DivisionRepositoy _divionRepositoy;
    private readonly ResidenceRepository _residenceRepository;

    public IndexModel(ApplicationDbContext context, DateService dateService,
        AreaRepository areaRepository, DivisionRepositoy divionRepositoy, ResidenceRepository residenceRepository)
    {
        _context = context;
        _dateService = dateService;
        _areaRepository = areaRepository;
        _divionRepositoy = divionRepositoy;
        _residenceRepository = residenceRepository;
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
            Residence = await _residenceRepository.GetResidencesAsync(username);
        }
        if (_context.Division != null)
        {
            Division = await _divionRepositoy.GetDivisionsAsync(username);
        }
        if (_context.Area != null)
        {
            Area = await _areaRepository.GetAreasAsync(username);
        }

    }
    public async Task<IActionResult> OnPostAsync(int divisionId)
    {
        string username = User.Identity.Name;

        if (!string.IsNullOrEmpty(username))
        {
            Residence = await _residenceRepository.GetResidencesAsync(username);
        }
        if (_context.Division != null)
        {
            Division = await _divionRepositoy.GetDivisionsAsync(username);
        }
        if (_context.Area != null)
        {
            Area = await _areaRepository.GetAreasAsync(username);
        }
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
                division.IsClean = true;
                _context.SaveChanges();
            }
            return RedirectToPage("./Index");
        }

        if (HttpContext.Request.Form.ContainsKey("simulador"))
        {
            int dias;
            var dataAtual2 = _dateService.ObterDataAtual();
            var dataSimulada = Date;

            TimeSpan diferenca = dataSimulada - dataAtual2;
            if ((int)diferenca.TotalDays > 0)
            {
                dias = (int)diferenca.TotalDays + 1; // adicionar dias 
            }
            else
            {
                dias = (int)diferenca.TotalDays; // retirar dias 
            }
            if (dias > 20)
            {
                Date = _dateService.ObterDataAtual();
                ViewData["DataAtual"] = _dateService.ObterDataAtual().ToString("dd/MM/yyyy HH:mm:ss");
                ModelState.AddModelError("Date", "Você não pode simular mais de 20 dias no futuro.");
                return Page();
            }
            if (dias != 0)
            {
                _dateService.IncrementarDataSimulation(dias);
            }
            return RedirectToPage("./Index");
        }
        return RedirectToPage("./Index");

    }

}
