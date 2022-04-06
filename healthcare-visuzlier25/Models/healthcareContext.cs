using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace healthcare_visuzlier25.Models
{
    public partial class healthcareContext : DbContext
    {
        public healthcareContext()
        {
        }

        public healthcareContext(DbContextOptions<healthcareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; } = null!;
        public virtual DbSet<DataUser> DataUsers { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonInTenant> PersonInTenants { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductActiveSubstanceMap> ProductActiveSubstanceMaps { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=healthcare;Username=mhassanin;Password=magical_password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<ActiveSubstance>(entity =>
            {
                entity.ToTable("active_substance");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ConcUnit).HasColumnName("conc_unit");

                entity.Property(e => e.Concentration).HasColumnName("concentration");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<DataUser>(entity =>
            {
                entity.ToTable("data_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Department).HasColumnName("department");

                entity.Property(e => e.EmailAddress).HasColumnName("email_address");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");

                entity.Property(e => e.MiddleName).HasColumnName("middle_name");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.DataUsers)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("tenant_id_fk");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.HasIndex(e => e.Email, "person_email_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.PersonType)
                    .HasColumnName("person_type")
                    .HasDefaultValueSql("'normal'::text");
            });

            modelBuilder.Entity<PersonInTenant>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("person_in_tenant");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("person_fk");

                entity.HasOne(d => d.Tenant)
                    .WithMany()
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tenant_fk");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AuthorizationNumber).HasColumnName("authorization_number");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasDefaultValueSql("'EG'::text");

                entity.Property(e => e.HolderName).HasColumnName("holder_name");

                entity.Property(e => e.PharmacuticalForm).HasColumnName("pharmacutical_form");

                entity.Property(e => e.ProductLifetime).HasColumnName("product_lifetime");

                entity.Property(e => e.ReRegistrationDate).HasColumnName("re_registration_date");

                entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");

                entity.Property(e => e.RouteOfAdminstration).HasColumnName("route_of_adminstration");
            });

            modelBuilder.Entity<ProductActiveSubstanceMap>(entity =>
            {
                entity.ToTable("product_active_substance_map");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.SubstanceId).HasColumnName("substance_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductActiveSubstanceMaps)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("product_id_fk");

                entity.HasOne(d => d.Substance)
                    .WithMany(p => p.ProductActiveSubstanceMaps)
                    .HasForeignKey(d => d.SubstanceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("substance_id_fk");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.ReportUrl).HasColumnName("report_url");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("created_by_person_fk");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("tenant_fk");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("tenant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
