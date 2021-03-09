using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Rating> Rating {get;set;}
        public virtual DbSet<RefreshToken> RefreshTokens{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server = tai.db.elephantsql.com; Port = 5432; Database = qrmsywrc; User Id = qrmsywrc; Password = 6bJzafq21RcePD2Md6WG-pcfiDqF8dzV");
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
