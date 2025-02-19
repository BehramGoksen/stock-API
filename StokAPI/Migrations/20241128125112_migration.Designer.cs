﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StokAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241128125112_migration")]
    partial class migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.35");

            modelBuilder.Entity("Entity.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardName")
                        .HasColumnType("TEXT");

                    b.Property<int>("CardQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("CardId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Entity.CardItem", b =>
                {
                    b.Property<int>("CardItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("itemquantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("CardItemId");

                    b.HasIndex("CardId");

                    b.HasIndex("ItemId");

                    b.ToTable("CardItems");
                });

            modelBuilder.Entity("Entity.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entity.CardItem", b =>
                {
                    b.HasOne("Entity.Card", "Card")
                        .WithMany("CardItems")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entity.Item", "Item")
                        .WithMany("CardItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Card");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Entity.Card", b =>
                {
                    b.Navigation("CardItems");
                });

            modelBuilder.Entity("Entity.Item", b =>
                {
                    b.Navigation("CardItems");
                });
#pragma warning restore 612, 618
        }
    }
}
