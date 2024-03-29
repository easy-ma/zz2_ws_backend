using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

            var user1 = new User()
            {
                Username = "Babidiii",
                Email = "babidiii@babidiii.babidiii",
                Id = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Babidiii"))
            };
            var user2 = new User()
            {
                Username = "Khaaz",
                Email = "khaaz@khaaz.khaaz",
                Id = new Guid("01fb32fd-21f9-4124-9758-3e042ce83a81"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Khaaz"))
            };
            var user3 = new User()
            {
                Username = "Xerstom",
                Email = "xerstom@xerstom.xerstom",
                Id = new Guid("098f568b-3d3c-4418-b300-aec1b67e06c9"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("Xerstom"))
            };
            var user4 = new User()
            {
                Username = "BasileNq",
                Email = "basilenq@basilenq.basilenq",
                Id = new Guid("728de876-cf01-4086-a031-fb7ae276c216"),
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("BasileNq"))
            };

            modelBuilder.Entity<User>().HasData(user1, user2, user3, user4);

            var refreshToken = new RefreshToken
            {
                Id = new Guid("0ee92631-6b36-45fe-817f-83259a4cc9e7"),
                Token = "babidiii-token",
                UserId = user1.Id
            };

            modelBuilder.Entity<RefreshToken>().HasData(refreshToken);


            var ad1 = new Ad()
            {
                Id = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876"),
                UserId = user1.Id,
                Name = "My Ad",
                Description = "My Description",
                Location = "My Location",
                Rate = 0,
                Price = 10,
                ImageURL = "MyUrl.png"
            };

            var ad2 = new Ad()
            {
                Id = new Guid("9fb98545-773f-4ca7-86c2-99ef1dd45876"),
                UserId = user1.Id,
                Name = "My Ad 2",
                Description = "My text about my ad",
                Location = "My Location",
                Rate = 0,
                Price = 10,
                ImageURL = "MyUrl.png"
            };

            var ad3 = new Ad()
            {
                Id = new Guid("66f6a39d-e550-400a-b3d9-e5a39b558523"),
                UserId = user2.Id,
                Name = "Different Ad",
                Description = "Different description",
                Location = "My Location 2",
                Rate = 0,
                Price = 50,
                ImageURL = "MyUrl.png"
            };

            modelBuilder.Entity<Ad>().HasData(ad1, ad2, ad3);

            var rate1 = new Rating()
            {
                Id = new Guid("6eb1eb3f-c2db-47e9-8afc-d6691c0baccc"),
                AdId = ad1.Id,
                UserId = user1.Id,
                Rate = 2,
                Comment = "My comment",
                Title = "My Title",
            };
            modelBuilder.Entity<Rating>().HasData(rate1);



            // add another entity here
        }
    }
}

