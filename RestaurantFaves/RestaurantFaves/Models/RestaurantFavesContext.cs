using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestaurantFaves.Models;

public partial class RestaurantFavesContext : DbContext
{
    public RestaurantFavesContext()
    {
    }

    public RestaurantFavesContext(DbContextOptions<RestaurantFavesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RestaurantRating> RestaurantRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Restaurant_Faves; Integrated Security=SSPI;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RestaurantRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC078415DF79");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Restaurant).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
