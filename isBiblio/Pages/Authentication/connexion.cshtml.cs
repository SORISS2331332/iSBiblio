using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using iSBiblio.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.Identity.Client;

namespace iSBiblio.Pages.Autentication
{
    
    public class connexionModel : PageModel
    {
        public bool error;
        public bool isDevelopmentMode = false;
        IConfiguration configuration;

        public connexionModel(IConfiguration configuration, IWebHostEnvironment env)
        {

            this.configuration = configuration;


            if (env.IsDevelopment())
            {
                isDevelopmentMode = true;
            }

        }
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("~/MenuLivres");
            }
           
            return Page();
        }
        private string GetUserRole(string email, SqlConnection connection)
        {
            // Implémentez une méthode pour récupérer le rôle de l'utilisateur à partir de la base de données
            string role = "User";  // Par défaut
            using (var command = new SqlCommand("SELECT Role FROM Utilisateurs WHERE Email = @Email", connection))
            {
                command.Parameters.Add(new SqlParameter("@Email", email));
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    role = result.ToString();
                }
            }
            return role;
        }
        public async Task<IActionResult> OnPostAsync(string email, string password, string ReturnUrl)
        {
            string con_str = "Server=isorgho;Database=Bibliotheque;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            
            using (var connection = new SqlConnection(con_str))
            {
                connection.Open();
                using (var command = new SqlCommand("AuthentifierUtilisateur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@MotDePasse", password));

                    var resultatParam = new SqlParameter("@Resultat", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultatParam);
                    command.ExecuteNonQuery();

                    bool resultat = (bool)resultatParam.Value;
                    if (resultat)
                    {
                        error = false;
                        string userRole = GetUserRole(email, connection); 

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, email),
                            new Claim(ClaimTypes.Role, userRole) 
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "Login");
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                            ClaimsPrincipal(claimsIdentity));
                        if (userRole == "Admin")
                        {
                            return Redirect(ReturnUrl ?? "~/Admin/Index");
                        }
                        else
                        {
                            return Redirect(ReturnUrl ?? "~/MenuLivres");
                        }

                    }
                    else
                    {
                        error = true;
                        return Page();
                    }
                }
            }


        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Authentication/connexion");
        }
    }
}
