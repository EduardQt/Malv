﻿// <auto-generated />
using System;
using Malv.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Malv.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230410141837_Chat_UserId")]
    partial class Chat_UserId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Malv.Data.EF.Entity.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Ad", (string)null);
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessage", (string)null);
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
                    b.HasOne("Malv.Data.EF.Entity.Category", "Category")
                        .WithMany("Ads")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malv.Data.EF.Entity.MalvUser", "User")
                        .WithMany("Ads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
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

                    b.Navigation("Chat");
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
                    b.Navigation("AdImages");

                    b.Navigation("CarAd")
                        .IsRequired();
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Category", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.Chat", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("Malv.Data.EF.Entity.MalvUser", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("UserData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
