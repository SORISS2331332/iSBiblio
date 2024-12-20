using iSBiblio.Data;
using iSBiblio.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace iSBiblio.Pages.Autentication
{
    public class InscriptionModel : PageModel
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; }
        private readonly BibliothequeContext _context;
        private readonly IConfiguration configuration;
        public InscriptionModel(BibliothequeContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        [BindProperty]
        public string MotDePasse { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //Connexion � la BD
            string con_str = configuration.GetConnectionString("DefaultConnection");
            try
            {
                //ex�cution de la proc�dure stock�e pour inscription de nouvel utilisateur
                using (var connection = new SqlConnection(con_str))
                {
                    connection.Open();
                    using (var command = new SqlCommand("AjouterUtilisateur", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Nom", Utilisateur.Nom));
                        command.Parameters.Add(new SqlParameter("@Prenom", Utilisateur.Prenom));
                        command.Parameters.Add(new SqlParameter("@Adresse", Utilisateur.Adresse));
                        command.Parameters.Add(new SqlParameter("@Email", Utilisateur.Email));
                        command.Parameters.Add(new SqlParameter("@MotDePasse", MotDePasse));
                        int nombre = command.ExecuteNonQuery();

                        if (nombre > 0)
                        {
                            Error = false;
                            return Redirect("/Authentication/connexion");

                        }
                        else
                        {
                            Error = true;
                            return Page();
                        }
                    }
                }
            }
            catch
            {
                Error = true;
                return Page();
            }
        }

    }


  
}
