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
        public string Message { get; set; }
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

        public async Task<IActionResult> OnPostAsync(string email, string password, string ReturnUrl)
        {
            string con_str = "Server=isorgho;Database=Bibliotheque;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            
            using (var connection = new SqlConnection(con_str))
            {
                connection.Open();
                using (var command = new SqlCommand("AuthentifierUtilisateur", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@MotDePasse", password));

                    var resultatParam = new SqlParameter("@Resultat", System.Data.SqlDbType.Bit)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(resultatParam);
                    command.ExecuteNonQuery();

                    bool resultat = (bool)resultatParam.Value;
                    if (resultat)
                    {
                        error = false;
                        var claims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Name, email)
                        };
                            var claimsIdentity = new ClaimsIdentity(claims, "Login");
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                            ClaimsPrincipal(claimsIdentity));
                            //Ca conduit vers la page où l'autorisation a été demandée
                            return Redirect(ReturnUrl == null ? "~/MenuLivres" : ReturnUrl);
                       
                    }
                    else
                    {
                        error = true;
                        Message = "Email ou mot de passe incorrect.";
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
