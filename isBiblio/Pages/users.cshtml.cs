using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iSBiblio.Pages
{
    public class UsersModel : PageModel
    {
        private readonly BibliothequeContext _context;
        public UsersModel(BibliothequeContext context)
        {
            _context = context;
        }

        public IList<Utilisateur> Utilisateurs { get; set; }

        public async Task OnGetAsync()
        {
            Utilisateurs = await _context.Utilisateurs.ToListAsync();
            foreach (var u in Utilisateurs) { Console.WriteLine(u); }
        }

    }
}
