using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iSBiblio.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly BibliothequeContext _context;

        public IndexModel(BibliothequeContext context)
        {
            _context = context;
        }
        public IList<VueEmpruntsActif> EmpruntsActifs { get; set; }
        public async Task OnGetAsync()
        {
            EmpruntsActifs = await _context.VueEmpruntsActifs.ToListAsync();
        }
    }
}
