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

namespace iSBiblio.Pages.Admin.Books
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public IndexModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        public IList<Livre> Livre { get;set; }

        public async Task OnGetAsync()
        {
            Livre = await _context.Livres
                .Include(l => l.Auteur).ToListAsync();
        }
    }
}
