﻿// <auto-generated />
using System;
using Malv.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Malv.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Malv.Data.EF.Entity.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("UserId");

                    b.ToTable("Ad", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AdCategory", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("AdImage", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdWatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("UserId");

                    b.ToTable("AdWatch", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.CarAd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CarType")
                        .HasColumnType("int");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("TransmissionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId")
                        .IsUnique();

                    b.ToTable("CarAd", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("UserId");

                    b.ToTable("Chat", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessage", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Country.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Country.Municipality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Municipality", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.MailToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TokenType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MailToken", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.MalvUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("MailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserData", (string)null);
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Ad", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Country.Municipality", "Municipality")
                        .WithMany("Ads")
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany("Ads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdCategory", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Ad", "Ad")
                        .WithMany("AdCategories")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.Category", "Category")
                        .WithMany("AdCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdImage", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Ad", "Ad")
                        .WithMany("AdImages")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.AdWatch", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Ad", "Ad")
                        .WithMany("AdWatches")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany("AdWatches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.CarAd", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Ad", "Ad")
                        .WithOne("CarAd")
                        .HasForeignKey("Malv.Data.EF.Entity.CarAd", "AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Category", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Chat", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Ad", "Ad")
                        .WithMany()
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.ChatMessage", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Chat", "Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany("ChatMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Country.Municipality", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.Country.Country", "Country")
                        .WithMany("Municipalities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.MailToken", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany("MailTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.UserData", b =>
                {
                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithOne("UserData")
                        .HasForeignKey("Malv.Data.EF.Entity.UserData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Ad", b =>
                {
                    b.Navigation("AdCategories");

                    b.Navigation("AdImages");

                    b.Navigation("AdWatches");

                    b.Navigation("CarAd")
                        .IsRequired();
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Category", b =>
                {
                    b.Navigation("AdCategories");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Chat", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Country.Country", b =>
                {
                    b.Navigation("Municipalities");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Country.Municipality", b =>
                {
                    b.Navigation("Ads");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.MalvUser", b =>
                {
                    b.Navigation("AdWatches");

                    b.Navigation("Ads");

                    b.Navigation("ChatMessages");

                    b.Navigation("MailTokens");

                    b.Navigation("UserData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
