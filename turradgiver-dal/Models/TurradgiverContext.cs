using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace turradgiver_dal.Models
{
    public partial class TurradgiverContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TurradgiverContext()
        {
        }

        public TurradgiverContext(DbContextOptions<TurradgiverContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Turradgiver"));
            }
        }
    }
}
