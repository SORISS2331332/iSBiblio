﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iSBiblio.Data;
using iSBiblio.Models;

namespace iSBiblio.Pages.Admin.Books
{
    public class DetailsModel : PageModel
    {
        private readonly iSBiblio.Data.BibliothequeContext _context;

        public DetailsModel(iSBiblio.Data.BibliothequeContext context)
        {
            _context = context;
        }

        public Livre Livre { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Livre = await _context.Livres
                .Include(l => l.Auteur).FirstOrDefaultAsync(m => m.LivreId == id);

            if (Livre == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}