using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CleanZone.Data;
using CleanZone.Data.Entities;

namespace CleanZone.Pages.Divisions
{
    public class EditModel : PageModel
    {
        private readonly CleanZone.Data.ApplicationDbContext _context;

        public EditModel(CleanZone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Division Division { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null || _context.Division == null)
            {
                return NotFound();
            }
            
            var division =  await _context.Division.FirstOrDefaultAsync(m => m.ID == id);
            if (division == null)
            {
                return NotFound();
            }
            Division = division;
           ViewData["AreaId"] = new SelectList(_context.Area, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }
            */
            _context.Attach(Division).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(Division.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DivisionExists(int id)
        {
          return (_context.Division?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
