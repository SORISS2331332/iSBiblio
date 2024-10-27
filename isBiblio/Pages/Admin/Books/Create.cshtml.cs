using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using iSBiblio.Data;
using iSBiblio.Models;

namespace iSBiblio.Pages.Admin.Books
{
    public class CreateModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public CreateModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "Nom");
            return Page();
        }

        [BindProperty]
        public Livre Livre { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Livres.Add(Livre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
