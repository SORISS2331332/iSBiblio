using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

public partial class Emprunt
{
    [Key]
    [Column("EmpruntID")]
    public int EmpruntId { get; set; }

    [Column("LivreID")]
    public int? LivreId { get; set; }

    [Column("UtilisateurID")]
    public int? UtilisateurId { get; set; }

    public DateOnly DateEmprunt { get; set; }

    public DateOnly? DateRetour { get; set; }

    public bool? EstRendu { get; set; }

    [ForeignKey("LivreId")]
    [InverseProperty("Emprunts")]
    public virtual Livre Livre { get; set; }

    [ForeignKey("UtilisateurId")]
    [InverseProperty("Emprunts")]
    public virtual Utilisateur Utilisateur { get; set; }
}
