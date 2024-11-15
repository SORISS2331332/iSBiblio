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
using System.Data.Common;
using System;
using Microsoft.Extensions.Logging;
using Dapper;

namespace iSBiblio.Pages.Autentication
{
    
    public class connexionModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public string Message {  get; set; }
        public connexionModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
      
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("~/categorie");
            }
           
            return Page();
        }
        private string GetUserRole(string email, SqlConnection connection)
        {
            
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
        public async Task<IActionResult> OnPostAsync(string ReturnUrl)
        {
            ReturnUrl ??= Url.Content("~/");
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                connection.Open();
                // Appel à la procédure stockée pour la connexion
                var parameters = new { Email = Input.Email, Password = Input.Password };
                var result = await _dbConnection.QuerySingleOrDefaultAsync<string>(
                    "dbo.ConnecterUtilisateur", parameters, commandType: CommandType.StoredProcedure);

                if (result == "Succès")
                {
                    string userRole = GetUserRole(Input.Email, connection);

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, Input.Email),
                            new Claim(ClaimTypes.Role, userRole)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                    ClaimsPrincipal(claimsIdentity));
                    if (userRole == "Admin")
                    {
                        return Redirect("~/Admin/Index");
                    }

                    return Redirect("/Index");
                }
                
                Message = result;
                return Page();
                


            }catch(Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

            
        }
       

        

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Authentication/connexion");
        }
    }
}
