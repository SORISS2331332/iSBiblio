using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

public partial class Livre
{
    [Key]
    [Column("LivreID")]
    public int LivreId { get; set; }

    [Required]
    [StringLength(255)]
    public string Titre { get; set; }

    [StringLength(255)]
    public string LienImage { get; set; }

    [Column("AuteurID")]
    public int AuteurId { get; set; }

    public int? AnneePublication { get; set; }

    [StringLength(100)]
    public string Genre { get; set; }

    public bool? Disponibilite { get; set; }

    [ForeignKey("AuteurId")]
    [InverseProperty("Livres")]
    public virtual Auteur Auteur { get; set; }

    [InverseProperty("Livre")]
    public virtual ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
}
