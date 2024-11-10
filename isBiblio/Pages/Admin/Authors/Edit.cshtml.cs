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

namespace iSBiblio.Pages.Admin.Authors
{
    public class EditModel : PageModel
    {
        private readonly BibliothequeContext _context;

        public EditModel(BibliothequeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Auteur Auteur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auteur = await _context.Auteurs.FirstOrDefaultAsync(m => m.AuteurId == id);

            if (Auteur == null)
            {
                return NotFound();
            }
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

            _context.Attach(Auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(Auteur.AuteurId))
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

        private bool AuteurExists(int id)
        {
            return _context.Auteurs.Any(e => e.AuteurId == id);
        }
    }
}
