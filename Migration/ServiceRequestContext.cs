using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ServiceRequestApp
{
    public partial class ServiceRequestContext : DbContext
    {
        public ServiceRequestContext()
        {
        }

        public ServiceRequestContext(DbContextOptions<ServiceRequestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Importance> Importance { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tema> Tema { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ServiceRequest;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Importance>(entity =>
            {
                entity.HasKey(e => e.IdImportance)
                    .HasName("Importance_pkey");

                entity.Property(e => e.IdImportance)
                    .HasColumnName("id_importance")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NameImportance)
                    .HasColumnName("name_importance")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("Request_pkey");

                entity.Property(e => e.IdRequest)
                    .HasColumnName("id_request")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateFinish)
                    .HasColumnName("date_finish")
                    .HasColumnType("date");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(400);

                entity.Property(e => e.DoneDuring).HasColumnName("done_during");

                entity.Property(e => e.Response)
                    .HasColumnName("response")
                    .HasMaxLength(400);

                entity.HasOne(d => d.Importance)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.ImportanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Request_ImportanceId_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Request_StatusId_fkey");

                entity.HasOne(d => d.Tema)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.TemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Request_TemaId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Request_UserId_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("Role_pkey");

                entity.Property(e => e.IdRole)
                    .HasColumnName("id_role")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NameRole)
                    .HasColumnName("name_role")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("Status_pkey");

                entity.Property(e => e.IdStatus)
                    .HasColumnName("id_status")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NameStatus)
                    .HasColumnName("name_status")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tema>(entity =>
            {
                entity.HasKey(e => e.IdTema)
                    .HasName("Tema_pkey");

                entity.Property(e => e.IdTema)
                    .HasColumnName("id_tema")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NameTema)
                    .HasColumnName("name_tema")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pkey");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(20);

                entity.Property(e => e.Middlename)
                    .HasColumnName("middlename")
                    .HasMaxLength(30);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(30);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(11);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_RoleId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
