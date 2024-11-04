using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iSBiblio.Pages
{
    [Authorize]
    public class ProfilModel : PageModel
    {
        private readonly BibliothequeContext _context;
        public string UserEmail { get;  set; }
        public Utilisateur UtilisateurConnected { get; set; }

        public ProfilModel(BibliothequeContext context)
        {
            _context = context;
        }
      
        public async Task OnGetAsync()
        {

            UserEmail = User.FindFirstValue(ClaimTypes.Name);
            UtilisateurConnected = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Email == UserEmail);
        }
    }
}
