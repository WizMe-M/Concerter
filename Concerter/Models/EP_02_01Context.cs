using System;
using System.Collections.Generic;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Concerter.Models
{
    // ReSharper disable once InconsistentNaming
    public partial class EP_02_01Context : DbContext
    {
        public EP_02_01Context()
        {
        }

        public EP_02_01Context(DbContextOptions<EP_02_01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CulturalBuilding> CulturalBuildings { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Giving> Givings { get; set; } = null!;
        public virtual DbSet<ParticipatingArtist> ParticipatingArtists { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=EP_02_01;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CulturalBuilding>(entity =>
            {
                entity.ToTable("cultural_buildings");

                entity.HasIndex(e => e.Name, "uq_cultural_buildings_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.HasIndex(e => e.Name, "uq_events_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost)
                    .HasPrecision(10, 2)
                    .HasColumnName("cost");

                entity.Property(e => e.CulturalBuildingId).HasColumnName("cultural_building_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ImpresarioId).HasColumnName("impresario_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.CulturalBuilding)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CulturalBuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_events_cultural_building_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_events_genre_id");

                entity.HasOne(d => d.Impresario)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ImpresarioId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_events_impresario_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_events_status_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_events_type_id");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.HasIndex(e => e.Name, "uq_genres_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Giving>(entity =>
            {
                entity.ToTable("givings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Place).HasColumnName("place");

                entity.Property(e => e.Prize)
                    .HasPrecision(10, 2)
                    .HasColumnName("prize");
            });

            modelBuilder.Entity<ParticipatingArtist>(entity =>
            {
                entity.ToTable("participating_artists");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ParticipatingArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_participating_artists_artist_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.ParticipatingArtists)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_participating_artists_event_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.Name, "uq_roles_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(35)
                    .HasColumnName("name");
            });
            modelBuilder.Entity<Role>().Ignore(e => e.AlterName);

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("statuses");

                entity.HasIndex(e => e.Name, "uq_statuses_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(11)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(30)
                    .HasColumnName("second_name");

                entity.HasKey(e => new { e.SecondName, e.FirstName, e.EventId });

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tickets_event_id");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("types");

                entity.HasIndex(e => e.Name, "uq_types_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(7)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "uq_users_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(16)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_users_role_id");
            });

            //initialize
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "cashier" },
                new Role { Id = 2, Name = "artist" },
                new Role { Id = 3, Name = "impresario" },
                new Role { Id = 4, Name = "organizer" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "org",
                    Password = "P@ssw0rd",
                    RoleId = 4,
                    FirstName = "Иван",
                    LastName = "Иванов"
                });

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Регистрация" },
                new Status { Id = 2, Name = "Набор" },
                new Status { Id = 3, Name = "Проведение" },
                new Status { Id = 4, Name = "Выплаты" },
                new Status { Id = 4, Name = "Продажа" }
            );

            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Концерт" },
                new Type { Id = 2, Name = "Конкурс" }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}