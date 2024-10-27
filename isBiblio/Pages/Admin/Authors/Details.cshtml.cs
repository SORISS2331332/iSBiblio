using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iSBiblio.Data;
using iSBiblio.Models;

namespace iSBiblio.Pages.Admin.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public DetailsModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

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
    }
}
