using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RegistrationService.Data.Entities
{
    public partial class postgresContext : DbContext
    {
        private string ConnectionString { get;set;}

        public postgresContext(string conString)
        {
            ConnectionString = conString;
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Transfer> Transfers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCred> UserCreds { get; set; } = null!;

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
                entity.ToTable("accounts", "bankapp");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.LName)
                    .HasColumnType("character varying")
                    .HasColumnName("l_name");

                entity.Property(e => e.RoutingNum)
                    .HasColumnName("routing_num")
                    .HasDefaultValueSql("((random() * (9)::double precision) + (1)::double precision)");

                entity.Property(e => e.UserFk).HasColumnName("user_fk");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accounts_user_fk_fkey");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("transfers", "bankapp");

                entity.Property(e => e.TransferId)
                    .HasColumnName("transfer_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AccountReceivedFk).HasColumnName("account_received_fk");

                entity.Property(e => e.AccountReleasedFk).HasColumnName("account_released_fk");

                entity.Property(e => e.AmountTransferred)
                    .HasColumnType("money")
                    .HasColumnName("amount_transferred");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.AccountReceivedFkNavigation)
                    .WithMany(p => p.TransferAccountReceivedFkNavigations)
                    .HasForeignKey(d => d.AccountReceivedFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transfers_account_received_fk_fkey");

                entity.HasOne(d => d.AccountReleasedFkNavigation)
                    .WithMany(p => p.TransferAccountReleasedFkNavigations)
                    .HasForeignKey(d => d.AccountReleasedFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transfers_account_released_fk_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "bankapp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .UseIdentityAlwaysColumn();

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
                    .HasColumnName("phone_num")
                    .IsFixedLength();

                entity.Property(e => e.UserAddress)
                    .HasColumnType("character varying")
                    .HasColumnName("user_address");
            });

            modelBuilder.Entity<UserCred>(entity =>
            {
                entity.ToTable("user_cred", "bankapp");

                entity.Property(e => e.UserCredId)
                    .HasColumnName("user_cred_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.PassHash)
                    .HasColumnType("character varying")
                    .HasColumnName("pass_hash");

                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.Property(e => e.UserFk).HasColumnName("user_fk");

                entity.Property(e => e.UserName)
                    .HasMaxLength(40)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.UserCreds)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_cred_user_fk_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
