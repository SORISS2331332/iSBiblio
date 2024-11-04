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

namespace iSBiblio.Pages.Admin
{
    
    public class ConnexionAdminModel : PageModel
    {
        public string Message { get; set; }
        public bool error;
        public ConnexionAdminModel()
        {

        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email, string password, string ReturnUrl)
        {
            if (email == "Adminis@gmail.com" && password == "Groums123")
            {
                error = false;
                var claims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Name, email)
                        };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Index" : ReturnUrl);

            }
            else
            {
                error = true;
                Message = "Email ou mot de passe incorrect.";
                return Page();
            }


        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/ConnexionAdmin");
        }
    }
}
