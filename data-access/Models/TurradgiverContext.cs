using System;
using System.Diagnostics;
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
                optionsBuilder.UseNpgsql("Server = kandula.db.elephantsql.com; Port = 5432; Database = vktoegob; User Id = vktoegob; Password = sCropnv82b1-7h4jNW3WC96AZNI9ub3d");
            }
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine("coucou");

            modelBuilder.Entity<Tests>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

        }*/

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
