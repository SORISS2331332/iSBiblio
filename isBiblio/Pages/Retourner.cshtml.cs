using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace iSBiblio.Pages
{
    [Authorize(Roles = "User")]
    public class RetournerModel : PageModel
    {
        private readonly BibliothequeContext _context;

        public RetournerModel(BibliothequeContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)] // Permet de lier l'ID lors d'une requête GET
        public int EmpruntID { get; set; }



        public Emprunt Emprunt { get; set; }
        public Utilisateur utilisateur { get; set; }

        public IActionResult OnGet()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Email == userEmail);


            string con_str = "Server=isorgho;Database=Bibliotheque;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

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
