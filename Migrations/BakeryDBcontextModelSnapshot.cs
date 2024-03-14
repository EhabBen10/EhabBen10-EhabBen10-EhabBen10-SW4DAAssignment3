﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SW4DAAssignment3.Data;

#nullable disable

namespace SW4DAAssignment3.Migrations
{
    [DbContext(typeof(BakeryDBcontext))]
    partial class BakeryDBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SW4DAAssignment3.Models.Allergen", b =>
                {
                    b.Property<int>("AllergenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AllergenId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AllergenId");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BakingGood", b =>
                {
                    b.Property<int>("BakingGoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BakingGoodId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BakingGoodId");

                    b.ToTable("BakingGoods");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Batch", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatchId"));

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Target_Finish_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("BatchId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BatchBackingGood", b =>
                {
                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("BakingGoodId")
                        .HasColumnType("int");

                    b.HasKey("BatchId", "BakingGoodId");

                    b.HasIndex("BakingGoodId");

                    b.ToTable("BatchBackingGoods");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BatchIngredient", b =>
                {
                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BatchId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("BatchIngredients");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.IngredientAllergen", b =>
                {
                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("AllergenId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId", "AllergenId");

                    b.HasIndex("AllergenId");

                    b.ToTable("IngredientAllergens");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("DeliveryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidityPeriod")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.OrderBakingGood", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("BakingGoodId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "BakingGoodId");

                    b.HasIndex("BakingGoodId");

                    b.ToTable("OrderBakingGoods");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.OrderSupermarket", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("SupermarketId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "SupermarketId");

                    b.HasIndex("SupermarketId");

                    b.ToTable("OrderSupermarkets");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Supermarket", b =>
                {
                    b.Property<int>("SupermarketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupermarketId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("offload_location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("track_id")
                        .HasColumnType("int");

                    b.HasKey("SupermarketId");

                    b.ToTable("Supermarkets");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Batch", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.Order", "Order")
                        .WithOne("Batch")
                        .HasForeignKey("SW4DAAssignment3.Models.Batch", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BatchBackingGood", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.BakingGood", "BakingGood")
                        .WithMany("BatchBackingGoods")
                        .HasForeignKey("BakingGoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW4DAAssignment3.Models.Batch", "Batch")
                        .WithMany("BatchBackingGoods")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BakingGood");

                    b.Navigation("Batch");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BatchIngredient", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.Batch", "Batch")
                        .WithMany("BatchIngredients")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW4DAAssignment3.Models.Ingredient", "Ingredient")
                        .WithMany("BatchIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.IngredientAllergen", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.Allergen", "Allergen")
                        .WithMany("IngredientAllergens")
                        .HasForeignKey("AllergenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW4DAAssignment3.Models.Ingredient", "Ingredient")
                        .WithMany("IngredientAllergens")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Allergen");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.OrderBakingGood", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.BakingGood", "BakingGood")
                        .WithMany("OrderBakingGoods")
                        .HasForeignKey("BakingGoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW4DAAssignment3.Models.Order", "Order")
                        .WithMany("OrderBakingGoods")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BakingGood");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.OrderSupermarket", b =>
                {
                    b.HasOne("SW4DAAssignment3.Models.Order", "Order")
                        .WithMany("OrderSupermarkets")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW4DAAssignment3.Models.Supermarket", "Supermarket")
                        .WithMany("OrderSupermarkets")
                        .HasForeignKey("SupermarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Supermarket");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Allergen", b =>
                {
                    b.Navigation("IngredientAllergens");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.BakingGood", b =>
                {
                    b.Navigation("BatchBackingGoods");

                    b.Navigation("OrderBakingGoods");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Batch", b =>
                {
                    b.Navigation("BatchBackingGoods");

                    b.Navigation("BatchIngredients");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Ingredient", b =>
                {
                    b.Navigation("BatchIngredients");

                    b.Navigation("IngredientAllergens");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Order", b =>
                {
                    b.Navigation("Batch");

                    b.Navigation("OrderBakingGoods");

                    b.Navigation("OrderSupermarkets");
                });

            modelBuilder.Entity("SW4DAAssignment3.Models.Supermarket", b =>
                {
                    b.Navigation("OrderSupermarkets");
                });
#pragma warning restore 612, 618
        }
    }
}
