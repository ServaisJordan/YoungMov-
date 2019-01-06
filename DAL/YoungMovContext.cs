using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL
{
    public partial class YoungMovContext : IdentityDbContext<User>
    {
        public YoungMovContext()
        {
        }

        public YoungMovContext(DbContextOptions<YoungMovContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Carpooling> Carpooling { get; set; }
        public virtual DbSet<CarpoolingApplicant> CarpoolingApplicant { get; set; }
        public virtual DbSet<PrivateMessage> PrivateMessage { get; set; }
        public virtual DbSet<TrustedCarpoolingDriver> TrustedCarpoolingDriver { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.HasIndex(e => e.LicensePlateNumber)
                    .HasName("UQ__car__41B4436DC0085C5D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarModel)
                    .IsRequired()
                    .HasColumnName("car_model")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LicensePlateNumber)
                    .IsRequired()
                    .HasColumnName("license_plate_number")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.ValidatedAt)
                    .HasColumnName("validated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__car__owner__12C8C788");
            });

            modelBuilder.Entity<Carpooling>(entity =>
            {
                entity.ToTable("carpooling");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Car).HasColumnName("car");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationFrom)
                    .IsRequired()
                    .HasColumnName("destination_from")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationTo)
                    .IsRequired()
                    .HasColumnName("destination_to")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LocalityFrom)
                    .IsRequired()
                    .HasColumnName("locality_from")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LocalityTo)
                    .IsRequired()
                    .HasColumnName("locality_to")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NbPlaces).HasColumnName("nb_places");

                entity.Property(e => e.PlacePrice)
                    .HasColumnName("place_price")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.PostalCodeFrom)
                    .IsRequired()
                    .HasColumnName("postalCode_From")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCodeTo)
                    .IsRequired()
                    .HasColumnName("postalCode_To")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p.Carpooling)
                    .HasForeignKey(d => d.Car)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__carpooling__car__15A53433");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.Carpooling)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__carpoolin__creat__1699586C");
            });

            modelBuilder.Entity<Carpooling>().HasMany(c => c.CarpoolingApplicant).WithOne(cA => cA.CarpoolingNavigation).HasForeignKey(cA => cA.Carpooling).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarpoolingApplicant>(entity =>
            {
                entity.ToTable("carpooling_applicant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carpooling).HasColumnName("carpooling");

                entity.Property(e => e.HasBeenAccepted).HasColumnName("has_been_accepted");

                entity.HasOne(d => d.CarpoolingNavigation)
                    .WithMany(p => p.CarpoolingApplicant)
                    .HasForeignKey(d => d.Carpooling)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__carpoolin__carpo__1975C517");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.CarpoolingApplicant)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__carpooling__User__1A69E950");
            });

            modelBuilder.Entity<PrivateMessage>(entity =>
            {
                entity.ToTable("private_message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.HasBeenRead).HasColumnName("has_been_read");

                entity.Property(e => e.Reponse).HasColumnName("reponse");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.PrivateMessage)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__private_m__creat__1E3A7A34");

                entity.HasOne(d => d.ReponseNavigation)
                    .WithMany(p => p.InverseReponseNavigation)
                    .HasForeignKey(d => d.Reponse)
                    .HasConstraintName("FK__private_m__repon__1F2E9E6D");
            });

            modelBuilder.Entity<TrustedCarpoolingDriver>(entity =>
            {
                entity.ToTable("trusted_carpooling_driver");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carpooler).HasColumnName("carpooler").IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.User).HasColumnName("user").IsRequired();

                entity.HasOne(d => d.CarpoolerNavigation)
                    .WithMany(p => p.TrustedCarpoolingDriverCarpoolerNavigation)
                    .HasForeignKey(d => d.Carpooler)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__trusted_c__carpo__0EF836A4");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.TrustedCarpoolingDriverUserNavigation)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__trusted_ca__user__0E04126B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__User__AB6E61641CE4AFE6")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__User__66DCF95CF8EE8657")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse)
                    .HasColumnName("adresse")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmailValidatedAt)
                    .HasColumnName("email_validated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FacePhotoFilename)
                    .HasColumnName("facePhoto_Filename")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FacePhotoSentAt)
                    .HasColumnName("facePhoto_sent_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FacePhotoValidatedAt)
                    .HasColumnName("facePhoto_validated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityPieceFilename)
                    .HasColumnName("identityPiece_filename")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityPieceSentAt)
                    .HasColumnName("identityPiece_sent_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdentityPieceValidatedAt)
                    .HasColumnName("identityPiece_validated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasColumnName("locality")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion();

                entity.Property(e => e.TrustedCarpoolingDriverCode)
                    .HasColumnName("trusted_carpooling_driver_code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<User>().HasMany(u => u.Carpooling).WithOne(c => c.CreatorNavigation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Car).WithOne(c => c.OwnerNavigation).HasForeignKey(c => c.Owner).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.TrustedCarpoolingDriverCarpoolerNavigation).WithOne(t => t.CarpoolerNavigation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.TrustedCarpoolingDriverUserNavigation).WithOne(t => t.UserNavigation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.PrivateMessage).WithOne(pm => pm.CreatorNavigation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.CarpoolingApplicant).WithOne(ca => ca.UserNavigation).HasForeignKey(ca => ca.User).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
