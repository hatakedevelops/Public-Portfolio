using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Data.Entities;

public partial class PostgresContext : DbContext
{
        private string ConnectionString { get;set;}

        public PostgresContext(string conString)
        {
            ConnectionString = conString;
        }
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }=null!;

    public virtual DbSet<Transfer> Transfers { get; set; }=null!;

    public virtual DbSet<User> Users { get; set; }=null!;

    public virtual DbSet<UserCred> UserCreds { get; set; }=null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("accounts_pkey");

            entity.ToTable("accounts", "bankapp");

            entity.Property(e => e.AccountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("account_id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.LName)
                .HasColumnType("character varying")
                .HasColumnName("l_name");
            entity.Property(e => e.RoutingNum)
                .HasDefaultValueSql("((random() * (9)::double precision) + (1)::double precision)")
                .HasColumnName("routing_num");
            entity.Property(e => e.UserFk).HasColumnName("user_fk");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_user_fk_fkey");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("transfers_pkey");

            entity.ToTable("transfers", "bankapp");

            entity.Property(e => e.TransferId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("transfer_id");
            entity.Property(e => e.AccountReceivedFk).HasColumnName("account_received_fk");
            entity.Property(e => e.AccountReleasedFk).HasColumnName("account_released_fk");
            entity.Property(e => e.AmountTransferred)
                .HasColumnType("money")
                .HasColumnName("amount_transferred");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");

            entity.HasOne(d => d.AccountReceivedFkNavigation).WithMany(p => p.TransferAccountReceivedFkNavigations)
                .HasForeignKey(d => d.AccountReceivedFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfers_account _received_fk_fkey");

            entity.HasOne(d => d.AccountReleasedFkNavigation).WithMany(p => p.TransferAccountReleasedFkNavigations)
                .HasForeignKey(d => d.AccountReleasedFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfers_account _released_fk_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user", "bankapp");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("f_name");
            entity.Property(e => e.LName)
                .HasMaxLength(50)
                .HasColumnName("l_name");
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_num");
            entity.Property(e => e.UserAddress)
                .HasColumnType("character varying")
                .HasColumnName("user_address");
        });

        modelBuilder.Entity<UserCred>(entity =>
        {
            entity.HasKey(e => e.UserCredId).HasName("user_cred_pkey");

            entity.ToTable("user_cred", "bankapp");

            entity.Property(e => e.UserCredId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_cred_id");
            entity.Property(e => e.PassHash)
                .HasColumnType("character varying")
                .HasColumnName("pass_hash");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.UserFk).HasColumnName("user_fk");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .HasColumnName("user_name");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.UserCreds)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_cred_user_fk_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
