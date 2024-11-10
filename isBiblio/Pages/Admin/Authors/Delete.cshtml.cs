using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;

namespace iSBiblio.Pages.Admin.Authors
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public DeleteModel(iSBiblio.Data.BibliothequeContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auteur = await _context.Auteurs.FindAsync(id);

            if (Auteur != null)
            {
                _context.Auteurs.Remove(Auteur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
