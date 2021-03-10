﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using turradgiver_dal.Models;

namespace turradgiver_dal.Migrations
{
    [DbContext(typeof(TurradgiverContext))]
    partial class TurradgiverContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("turradgiver_dal.Models.Ad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("ImageURL")
                        .HasColumnType("text")
                        .HasColumnName("ImageURL");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<float>("Price")
                        .HasColumnType("real")
                        .HasColumnName("Price");

                    b.Property<float>("Rate")
                        .HasColumnType("real")
                        .HasColumnName("Rate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("turradgiver_dal.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("AdId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("Comment");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<int>("Rate")
                        .HasColumnType("integer")
                        .HasColumnName("Rate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("turradgiver_dal.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("Token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("turradgiver_dal.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<byte[]>("Password")
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("turradgiver_dal.Models.Ad", b =>
                {
                    b.HasOne("turradgiver_dal.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("turradgiver_dal.Models.Rating", b =>
                {
                    b.HasOne("turradgiver_dal.Models.Ad", "Ad")
                        .WithMany()
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("turradgiver_dal.Models.RefreshToken", b =>
                {
                    b.HasOne("turradgiver_dal.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
