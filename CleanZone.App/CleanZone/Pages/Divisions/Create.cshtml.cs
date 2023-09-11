using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanZone.Data;
using CleanZone.Data.Entities;

namespace CleanZone.Pages.Divisions;

public class CreateModel : PageModel
{
    private readonly CleanZone.Data.ApplicationDbContext _context;

    public CreateModel(CleanZone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        string username = User.Identity.Name;

        // Filtrar as residências pertencentes ao usuário pelo nome do usuário
        var userResidences = _context.Area
            .Where(r => r.Residence.User.UserName == username)
            .ToList();
        ViewData["AreaId"] = new SelectList(userResidences, "Id", "Id");
        return Page();
    }

    [BindProperty]
    public Division Division { get; set; } = default!;
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        /*
      if (!ModelState.IsValid || _context.Division == null || Division == null)
        {
            return Page();
        }
        */
        _context.Division.Add(Division);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
