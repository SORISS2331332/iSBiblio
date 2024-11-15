using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSBiblio.Pages
{
    public class RechercherModel : PageModel
    {


        private readonly BibliothequeContext _context;
        private readonly IConfiguration _configuration;

        public RechercherModel(BibliothequeContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [BindProperty(SupportsGet = true)]
        public string TextSaisi { get; set; }
        public IList<Livre> Resultats { get; set; } = new List<Livre>();


        public void OnGet(string textSaisi)
        {
            TextSaisi = textSaisi;
            if (!string.IsNullOrEmpty(TextSaisi))
            {
                var textSaisiLower = TextSaisi.ToLower();
                Resultats = _context.Livres
                    .Where(b => (b.Titre.ToLower().Contains(TextSaisi.ToLower()) ||
                                 b.Genre.ToLower().Contains(TextSaisi.ToLower()) ||
                                 b.Auteur.Prenom.ToLower().Contains(TextSaisi.ToLower()) ||
                                 b.Auteur.Nom.ToLower().Contains(TextSaisi.ToLower())))
                    .Include(b => b.Auteur)
                    .ToList();

                

            }

        }
    }
}
