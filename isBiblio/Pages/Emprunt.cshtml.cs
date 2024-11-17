using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iSBiblio.Pages
{
    [Authorize(Roles = "User")]
    public class EmpruntModel : PageModel
    {

        private readonly BibliothequeContext _context;
        public string Message { get; private set; }
        public EmpruntModel(BibliothequeContext context)
        {
            _context = context;
        }

        public IList<VueEmpruntsUser> userEmprunts { get; set; }


        public async Task OnGetAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            Message = TempData["Message"] as string; //recuperer un message par get
            if (userEmail != null)
            {
                //Requête pour selectionner les emprunts en cours
                userEmprunts = await _context.VueEmpruntsUsers.Where(e => e.Email == userEmail).ToListAsync();
                
            }
            
        }
    }
}
