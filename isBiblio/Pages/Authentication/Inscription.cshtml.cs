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

namespace iSBiblio.Pages.Autentication
{
    public class InscriptionModel : PageModel
    {
        public bool Error;
        public string Message { get; set; }
        private readonly BibliothequeContext _context;

        public InscriptionModel(BibliothequeContext context)
        {
            _context = context;
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
           
            string con_str = "Server=isorgho;Database=Bibliotheque;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            try
            {
                using (var connection = new SqlConnection(con_str))
                {
                    connection.Open();
                    using (var command = new SqlCommand("AjouterUtilisateur", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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
            catch (SqlException ex)
            {
                Message = $"Erreur SQL: {ex.Message}";
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"Erreur SQL: {ex.Message}";
                return Page();
            }
        }

    }


  
}
