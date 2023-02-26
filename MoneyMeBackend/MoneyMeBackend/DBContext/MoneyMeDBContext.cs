using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoneyMeBackend.DBContext
{
    public partial class MoneyMeDBContext : DbContext
    {
        public MoneyMeDBContext()
        {
        }

        public MoneyMeDBContext(DbContextOptions<MoneyMeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blacklist> Blacklists { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<Quote> Quotes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NOLZ-DESKTOP;Database=MoneyMeDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blacklist>(entity =>
            {
                entity.ToTable("Blacklist");

                entity.Property(e => e.Domain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.Property(e => e.LoanId).HasColumnName("LoanID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EstablishmentFee).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FinanceAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Interest).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepaymentsFrom).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalRepayments).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Loan__CustomerID__778AC167");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("Quote");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.AmountRequired).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Quote__CustomerI__71D1E811");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
