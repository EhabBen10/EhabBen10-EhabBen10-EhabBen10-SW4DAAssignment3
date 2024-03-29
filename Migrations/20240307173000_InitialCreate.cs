﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SW4DAAssignment3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BakingGoods",
                columns: table => new
                {
                    BakingGoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingGoods", x => x.BakingGoodId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Supermarkets",
                columns: table => new
                {
                    SupermarketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offload_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    track_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supermarkets", x => x.SupermarketId);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Target_Finish_Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_Batches_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBakingGoods",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BakingGoodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBakingGoods", x => new { x.OrderId, x.BakingGoodId });
                    table.ForeignKey(
                        name: "FK_OrderBakingGoods_BakingGoods_BakingGoodId",
                        column: x => x.BakingGoodId,
                        principalTable: "BakingGoods",
                        principalColumn: "BakingGoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBakingGoods_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSupermarkets",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    SupermarketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSupermarkets", x => new { x.OrderId, x.SupermarketId });
                    table.ForeignKey(
                        name: "FK_OrderSupermarkets_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSupermarkets_Supermarkets_SupermarketId",
                        column: x => x.SupermarketId,
                        principalTable: "Supermarkets",
                        principalColumn: "SupermarketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchBackingGoods",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    BakingGoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchBackingGoods", x => new { x.BatchId, x.BakingGoodId });
                    table.ForeignKey(
                        name: "FK_BatchBackingGoods_BakingGoods_BakingGoodId",
                        column: x => x.BakingGoodId,
                        principalTable: "BakingGoods",
                        principalColumn: "BakingGoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchBackingGoods_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchIngredients",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchIngredients", x => new { x.BatchId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_BatchIngredients_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchBackingGoods_BakingGoodId",
                table: "BatchBackingGoods",
                column: "BakingGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_OrderId",
                table: "Batches",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchIngredients_IngredientId",
                table: "BatchIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBakingGoods_BakingGoodId",
                table: "OrderBakingGoods",
                column: "BakingGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupermarkets_SupermarketId",
                table: "OrderSupermarkets",
                column: "SupermarketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchBackingGoods");

            migrationBuilder.DropTable(
                name: "BatchIngredients");

            migrationBuilder.DropTable(
                name: "OrderBakingGoods");

            migrationBuilder.DropTable(
                name: "OrderSupermarkets");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "BakingGoods");

            migrationBuilder.DropTable(
                name: "Supermarkets");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
