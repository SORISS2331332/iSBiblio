using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iSBiblio.Models;

[Keyless]
public partial class VueEmpruntsUser
{
    [Column("EmpruntID")]
    public int EmpruntId { get; set; }

    [Required]
    [StringLength(255)]
    public string Titre { get; set; }

    [StringLength(100)]
    public string Genre { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    public DateOnly DateEmprunt { get; set; }

    public DateOnly? DateRetour { get; set; }
}
