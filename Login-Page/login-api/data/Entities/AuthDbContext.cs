using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace login_api.data.Entities;

public partial class AuthDbContext : DbContext
{

    private string ConnectionString { get;set;}

    public AuthDbContext(string conString)
    {
        ConnectionString = conString;
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(1000L, null, null, null, null, null)
                .HasColumnName("userid");
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
