using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;

namespace iSBiblio.Pages.Admin.Authors
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public CreateModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Auteur Auteur { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Auteurs.Add(Auteur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
