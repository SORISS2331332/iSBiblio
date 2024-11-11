using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;

namespace iSBiblio.Pages
{
    [Authorize(Roles = "User")]
    public class RetournerModel : PageModel
    {
        private readonly BibliothequeContext _context;
        private readonly IConfiguration configuration;

        public RetournerModel(BibliothequeContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [BindProperty(SupportsGet = true)] // Permet de lier l'ID lors d'une requête GET
        public int EmpruntID { get; set; }



        public Emprunt Emprunt { get; set; }
        public Utilisateur utilisateur { get; set; }

        public IActionResult OnGet(int id)
        {
            EmpruntID = id;
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Email == userEmail);


            string con_str = configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(con_str))
            {
                connection.Open();
                using (var command = new SqlCommand("RetournerLivre", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmpruntID", EmpruntID));
                    int row = command.ExecuteNonQuery();

                    if (row > 0)
                    {
                        TempData["Message"] = "Merci ! Vous avez retourné un livre avec succès !";
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
