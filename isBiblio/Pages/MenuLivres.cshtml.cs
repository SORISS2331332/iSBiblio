using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iSBiblio.Pages
{
    public class MenuLivresModel : PageModel
    {
        private readonly BibliothequeContext _context;
        public bool IsAuthenticated { get; private set; }

        
        public MenuLivresModel(BibliothequeContext context)
        {
            _context = context;
        }
        public IList<LivresDisponible> Livres { get; set; }
        public async Task  OnGetAsync()
        {
            
            IsAuthenticated = User.Identity.IsAuthenticated;
            Livres = await _context.LivresDisponibles.ToListAsync();
        }
    }
}
