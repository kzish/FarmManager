using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmManager.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<MLogs> MLogs { get; set; }
        public virtual DbSet<MRabbits> MRabbits { get; set; }
        public virtual DbSet<MRabbitsDeathsRecords> MRabbitsDeathsRecords { get; set; }
        public virtual DbSet<MRabbitsDeseasesAndTreatmentNotes> MRabbitsDeseasesAndTreatmentNotes { get; set; }
        public virtual DbSet<MRabbitsFemaleDoeBreadersRecords> MRabbitsFemaleDoeBreadersRecords { get; set; }
        public virtual DbSet<MRabbitsNotes> MRabbitsNotes { get; set; }
        public virtual DbSet<MRabbitsSalesRecords> MRabbitsSalesRecords { get; set; }
        public virtual DbSet<MRabbitsTreatmentRecords> MRabbitsTreatmentRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=FarmManager;User Id=sa;Password=abc123!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<MLogs>(entity =>
            {
                entity.ToTable("m_logs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data1)
                    .HasColumnName("data1")
                    .HasColumnType("text");

                entity.Property(e => e.Data2)
                    .HasColumnName("data2")
                    .HasColumnType("text");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MRabbits>(entity =>
            {
                entity.ToTable("m_rabbits");

                entity.HasIndex(e => e.NameId)
                    .HasName("unique_name_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DameId)
                    .IsRequired()
                    .HasColumnName("dame_id")
                    .HasMaxLength(10);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.LitterNumber).HasColumnName("litter_number");

                entity.Property(e => e.NameId)
                    .IsRequired()
                    .HasColumnName("name_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.SireId)
                    .IsRequired()
                    .HasColumnName("sire_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MRabbitsDeathsRecords>(entity =>
            {
                entity.ToTable("m_rabbits_deaths_records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfDeath)
                    .HasColumnName("date_of_death")
                    .HasColumnType("datetime");

                entity.Property(e => e.NameId)
                    .IsRequired()
                    .HasColumnName("name_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<MRabbitsDeseasesAndTreatmentNotes>(entity =>
            {
                entity.ToTable("m_rabbits_deseases_and_treatment_notes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Desease)
                    .IsRequired()
                    .HasColumnName("desease")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.Symptoms)
                    .IsRequired()
                    .HasColumnName("symptoms")
                    .HasColumnType("text");

                entity.Property(e => e.Treatment)
                    .IsRequired()
                    .HasColumnName("treatment")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<MRabbitsFemaleDoeBreadersRecords>(entity =>
            {
                entity.ToTable("m_rabbits_female_doe_breaders_records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActualKindlingDate)
                    .HasColumnName("actual_kindling_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActualWeeningDate)
                    .HasColumnName("actual_weening_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateMated)
                    .HasColumnName("date_mated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deaths).HasColumnName("deaths");

                entity.Property(e => e.LitterSize).HasColumnName("litter_size");

                entity.Property(e => e.NameIdBuck)
                    .IsRequired()
                    .HasColumnName("name_id_buck")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameIdDoe)
                    .IsRequired()
                    .HasColumnName("name_id_doe")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<MRabbitsNotes>(entity =>
            {
                entity.ToTable("m_rabbits_notes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MRabbitsSalesRecords>(entity =>
            {
                entity.ToTable("m_rabbits_sales_records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuyerAddress)
                    .HasColumnName("buyer_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerEmail)
                    .HasColumnName("buyer_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerMobile)
                    .HasColumnName("buyer_mobile")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerName)
                    .HasColumnName("buyer_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateSold)
                    .HasColumnName("date_sold")
                    .HasColumnType("datetime");

                entity.Property(e => e.NameId)
                    .IsRequired()
                    .HasColumnName("name_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.Pedigree).HasColumnName("pedigree");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeeksOld).HasColumnName("weeks_old");

                entity.Property(e => e.WeightKg)
                    .HasColumnName("weight_kg")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<MRabbitsTreatmentRecords>(entity =>
            {
                entity.ToTable("m_rabbits_treatment_records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateTreated)
                    .HasColumnName("date_treated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Illness)
                    .IsRequired()
                    .HasColumnName("illness")
                    .HasColumnType("text");

                entity.Property(e => e.NameId)
                    .IsRequired()
                    .HasColumnName("name_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.Treatment)
                    .IsRequired()
                    .HasColumnName("treatment")
                    .HasColumnType("text");
            });
        }
    }
}
