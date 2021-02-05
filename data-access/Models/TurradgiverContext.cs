using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class TurradgiverContext : DbContext
    {

        public TurradgiverContext()
        {
        }

        public TurradgiverContext(DbContextOptions<TurradgiverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Test> Tests { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            //modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


        //     modelBuilder.Entity<Customer>(entity =>
        //     {
        //         entity.ToTable("customers", "sales");

        //         entity.Property(e => e.CustomerId).HasColumnName("customer_id");

        //         entity.Property(e => e.City)
        //             .HasMaxLength(50)
        //             .IsUnicode(false)
        //             .HasColumnName("city");

        //         entity.Property(e => e.Email)
        //             .IsRequired()
        //             .HasMaxLength(255)
        //             .IsUnicode(false)
        //             .HasColumnName("email");

        //         entity.Property(e => e.FirstName)
        //             .IsRequired()
        //             .HasMaxLength(255)
        //             .IsUnicode(false)
        //             .HasColumnName("first_name");

        //         entity.Property(e => e.LastName)
        //             .IsRequired()
        //             .HasMaxLength(255)
        //             .IsUnicode(false)
        //             .HasColumnName("last_name");

        //         entity.Property(e => e.Phone)
        //             .HasMaxLength(25)
        //             .IsUnicode(false)
        //             .HasColumnName("phone");

        //         entity.Property(e => e.State)
        //             .HasMaxLength(25)
        //             .IsUnicode(false)
        //             .HasColumnName("state");

        //         entity.Property(e => e.Street)
        //             .HasMaxLength(255)
        //             .IsUnicode(false)
        //             .HasColumnName("street");

        //         entity.Property(e => e.ZipCode)
        //             .HasMaxLength(5)
        //             .IsUnicode(false)
        //             .HasColumnName("zip_code");
        //     });

            modelBuilder.Entity<Test>(entity =>
            {
                
            });

         }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
