using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

public partial class Auteur
{
    [Key]
    [Column("AuteurID")]
    public int AuteurId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nom { get; set; }

    [Required]
    [StringLength(100)]
    public string Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }

    [InverseProperty("Auteur")]
    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
}
