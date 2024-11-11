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
                Resultats = _context.Livres.Where(b => (b.Titre.Contains(TextSaisi) || b.Genre.Contains(TextSaisi) || b.Auteur.Prenom.Contains(TextSaisi) || b.Auteur.Nom.Contains(TextSaisi))).Include(b => b.Auteur).ToList();
                
            }
           
        }
    }
}
