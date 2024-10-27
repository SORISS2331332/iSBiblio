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
    public class IndexModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public IndexModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        public IList<Auteur> Auteur { get;set; }

        public async Task OnGetAsync()
        {
            Auteur = await _context.Auteurs.ToListAsync();
        }
    }
}
