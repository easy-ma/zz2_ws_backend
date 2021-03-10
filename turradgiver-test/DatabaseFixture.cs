using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Data.Common;
using turradgiver_dal.Models;

namespace turradgiver_test
{

    public class DbContextFixture : TurradgiverContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user1 = new User("Babidiii", "babidiii@babidiii.babidiii"){
                Id = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Babidiii"))
            };
            var user2 = new User("Khaaz", "khaaz@khaaz.khaaz"){
                Id = new Guid("01fb32fd-21f9-4124-9758-3e042ce83a81"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Khaaz"))
            };
            var user3 = new User("Xerstom", "xerstom@xerstom.xerstom"){
                Id = new Guid("098f568b-3d3c-4418-b300-aec1b67e06c9"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Xerstom"))
            };
            var user4 = new User("BasileNq", "basilenq@basilenq.basilenq"){
                Id = new Guid("728de876-cf01-4086-a031-fb7ae276c216"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("BasileNq"))
            };
        
            modelBuilder.Entity<User>().HasData(user1, user2, user3, user4);

            var refreshToken = new RefreshToken{
                Id = new Guid("0ee92631-6b36-45fe-817f-83259a4cc9e7"),
                Token = "babidiii-token",
                UserId = user1.Id
            };

            modelBuilder.Entity<RefreshToken>().HasData(refreshToken);
            

            // add another entity here
        }
    }
}
