using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iSBiblio.Data;
using iSBiblio.Models;

namespace iSBiblio.Pages.Admin.Books
{
    public class EditModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public EditModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Livre Livre { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Livre = await _context.Livres
                .Include(l => l.Auteur).FirstOrDefaultAsync(m => m.LivreId == id);

            if (Livre == null)
            {
                return NotFound();
            }
           ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(Livre.LivreId))
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

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.LivreId == id);
        }
    }
}
