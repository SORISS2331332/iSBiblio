using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

[Keyless]
public partial class LivresDisponible
{
    [Column("LivreID")]
    public int LivreId { get; set; }

    [Required]
    [StringLength(255)]
    public string Titre { get; set; }

    [StringLength(100)]
    public string Genre { get; set; }

    public DateOnly? AnneePublication { get; set; }

    [Required]
    [StringLength(100)]
    public string Auteur { get; set; }

    [Required]
    [StringLength(100)]
    public string PrenomAuteur { get; set; }
}
