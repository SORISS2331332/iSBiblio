using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

[Index("Email", Name = "UQ__Utilisat__A9D105348408A5CA", IsUnique = true)]
public partial class Utilisateur
{
    [Key]
    [Column("UtilisateurID")]
    public int UtilisateurId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nom { get; set; }

    [Required]
    [StringLength(100)]
    public string Prenom { get; set; }

    [StringLength(255)]
    public string Adresse { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public byte[] MotDePasse { get; set; }
    public string Role { get; set; }

    public DateOnly? DateInscription { get; set; }

    public Guid? Sel { get; set; }

    [InverseProperty("Utilisateur")]
    public virtual ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
}
