using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using iSBiblio.Models;

namespace iSBiblio.Data;

public partial class BibliothequeContext : DbContext
{
    public BibliothequeContext()
    {
    }

    public BibliothequeContext(DbContextOptions<BibliothequeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auteur> Auteurs { get; set; }

    public virtual DbSet<Emprunt> Emprunts { get; set; }

    public virtual DbSet<Livre> Livres { get; set; }

    public virtual DbSet<LivresDisponible> LivresDisponibles { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VueEmpruntsActif> VueEmpruntsActifs { get; set; }

    public virtual DbSet<VueEmpruntsUser> VueEmpruntsUsers { get; set; }

    public virtual DbSet<VueLivresDisponible> VueLivresDisponibles { get; set; }

    public virtual DbSet<VueUtilisateursAvecLivre> VueUtilisateursAvecLivres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auteur>(entity =>
        {
            entity.HasKey(e => e.AuteurId).HasName("PK__Auteurs__757A49A29F63A9D3");
        });

        modelBuilder.Entity<Emprunt>(entity =>
        {
            entity.HasKey(e => e.EmpruntId).HasName("PK__Emprunts__629ED277945F6F1E");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_MettreAJourRetourLivre");
                    tb.HasTrigger("trg_VerifierDisponibiliteLivre");
                });

            entity.Property(e => e.EstRendu).HasDefaultValue(false);

            entity.HasOne(d => d.Livre).WithMany(p => p.Emprunts).HasConstraintName("FK__Emprunts__LivreI__4222D4EF");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Emprunts).HasConstraintName("FK__Emprunts__Utilis__4316F928");
        });

        modelBuilder.Entity<Livre>(entity =>
        {
            entity.HasKey(e => e.LivreId).HasName("PK__Livres__562AE7E7D94427E0");

            entity.Property(e => e.Disponibilite).HasDefaultValue(true);
            entity.Property(e => e.LienImage).HasDefaultValue("img/couverture.png");

            entity.HasOne(d => d.Auteur).WithMany(p => p.Livres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Livres__AuteurID__3E52440B");
        });

        modelBuilder.Entity<LivresDisponible>(entity =>
        {
            entity.ToView("LivresDisponibles");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.UtilisateurId).HasName("PK__Utilisat__6CB6AE1F4151F8DF");

            entity.Property(e => e.DateInscription).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<VueEmpruntsActif>(entity =>
        {
            entity.ToView("VueEmpruntsActifs");
        });

        modelBuilder.Entity<VueEmpruntsUser>(entity =>
        {
            entity.ToView("VueEmpruntsUser");
        });

        modelBuilder.Entity<VueLivresDisponible>(entity =>
        {
            entity.ToView("VueLivresDisponibles");
        });

        modelBuilder.Entity<VueUtilisateursAvecLivre>(entity =>
        {
            entity.ToView("VueUtilisateursAvecLivres");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
