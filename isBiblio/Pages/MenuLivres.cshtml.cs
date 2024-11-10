using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


        public async Task  OnGetAsync(string name)
        {
            
            IsAuthenticated = User.Identity.IsAuthenticated;
            if(name == null)
            {
                Livres = await _context.LivresDisponibles.ToListAsync(); //Afficher toutes les catégories de livres
            }
            else
            {
                Livres = await _context.LivresDisponibles.Where(livre=>livre.Genre == name).ToListAsync(); //Afficher les livres par catégorie passé en param
            }
            
        }
    }
}
