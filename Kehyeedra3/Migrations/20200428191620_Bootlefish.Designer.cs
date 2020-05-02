﻿// <auto-generated />
using System;
using Kehyeedra3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kehyeedra3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200428191620_Bootlefish")]
    partial class Bootlefish
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kehyeedra3.Services.Models.BattleFish", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<byte>("Equipment")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("FishType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("Lvl")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("NextXp")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Xp")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("BattleFish");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.Fishing", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Inventory")
                        .HasColumnType("LONGTEXT");

                    b.Property<ulong>("LastFish")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Lvl")
                        .HasColumnType("bigint unsigned");

                    b.Property<byte>("RodOwned")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("RodUsed")
                        .HasColumnType("tinyint unsigned");

                    b.Property<ulong>("TXp")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Xp")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("Fishing");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.ItemOffer", b =>
                {
                    b.Property<ulong>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<ulong>("BuyerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("IsPurchaseFromStore")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSellOffer")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("ItemId")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("OfferAmount")
                        .HasColumnType("int");

                    b.Property<ulong?>("StoreFrontId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("StoreId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("OfferId");

                    b.HasIndex("StoreFrontId");

                    b.ToTable("ItemOffer");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.Reminder", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Created")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("Send")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.StoreFront", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("StoreItemType")
                        .HasColumnType("int");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("StoreFronts");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.StoreInventory", b =>
                {
                    b.Property<ulong>("InvId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Item")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<ulong?>("StoreFrontId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("InvId");

                    b.HasIndex("StoreFrontId");

                    b.ToTable("StoreInventory");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.User", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("GeneralInventory")
                        .HasColumnType("LONGTEXT");

                    b.Property<ulong>("LastMine")
                        .HasColumnType("bigint unsigned");

                    b.Property<long>("Money")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.ItemOffer", b =>
                {
                    b.HasOne("Kehyeedra3.Services.Models.StoreFront", null)
                        .WithMany("Offers")
                        .HasForeignKey("StoreFrontId");
                });

            modelBuilder.Entity("Kehyeedra3.Services.Models.StoreInventory", b =>
                {
                    b.HasOne("Kehyeedra3.Services.Models.StoreFront", null)
                        .WithMany("Items")
                        .HasForeignKey("StoreFrontId");
                });
#pragma warning restore 612, 618
        }
    }
}
