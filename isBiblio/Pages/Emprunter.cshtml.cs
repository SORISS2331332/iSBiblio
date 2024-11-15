using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace iSBiblio.Pages
{
    [Authorize(Roles = "User")]
    public class EmprunterModel : PageModel
    {
        private readonly BibliothequeContext _context;
        private readonly IConfiguration _configuration;

        public EmprunterModel(BibliothequeContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)] // Permet de lier l'ID lors d'une requête GET
        public int LivreId { get; set; }

        

        public Emprunt Emprunt { get; set; }
        public Utilisateur utilisateur {  get; set; }

        public IActionResult OnGet(int id)
        {
            LivreId = id;
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Email == userEmail);


            string con_str = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(con_str))
            {
                connection.Open();
                using (var command = new SqlCommand("EmprunterLivre", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@LivreID", LivreId));
                    command.Parameters.Add(new SqlParameter("@UtilisateurID", utilisateur.UtilisateurId));
                    int row = command.ExecuteNonQuery();

                    if (row > 0)
                    {
                        TempData["Message"] = "Vous avez emprunté un livre avec succès !";
                        return Redirect("/Emprunt");

                    }
                    else
                    {
                        return RedirectToPage("/MenuLivres");
                    }
                }
            }

        }
    }
}
