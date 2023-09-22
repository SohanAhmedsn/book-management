using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookOrders",
                columns: table => new
                {
                    BookOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookOrderDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrders", x => x.BookOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    BookType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalPublisher = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                    table.ForeignKey(
                        name: "FK_Publishers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "BookName", "BookType", "Picture", "Price" },
                values: new object[,]
                {
                    { 1, "Book01", 1, "1.jpg", 950.00m },
                    { 2, "Book02", 1, "2.jpg", 1900.00m },
                    { 3, "Book03", 1, "3.jpg", 2850.00m },
                    { 4, "Book04", 1, "4.jpg", 3800.00m },
                    { 5, "Book05", 1, "5.jpg", 4750.00m },
                    { 6, "Book06", 1, "6.jpg", 5700.00m },
                    { 7, "Book07", 1, "7.jpg", 6650.00m }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "BookId", "PublisherDate", "PublisherName", "TotalPublisher" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 8, 30, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2434), "Publisher01", 50 },
                    { 2, 2, new DateTime(2023, 8, 7, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2469), "Publisher02", 100 },
                    { 3, 3, new DateTime(2023, 7, 15, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2478), "Publisher03", 150 },
                    { 4, 4, new DateTime(2023, 6, 22, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2487), "Publisher04", 200 },
                    { 5, 5, new DateTime(2023, 5, 30, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2495), "Publisher05", 250 },
                    { 6, 6, new DateTime(2023, 5, 7, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2505), "Publisher06", 300 },
                    { 7, 7, new DateTime(2023, 4, 14, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2513), "Publisher07", 350 },
                    { 8, 1, new DateTime(2023, 3, 22, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2522), "Publisher08", 400 },
                    { 9, 2, new DateTime(2023, 2, 27, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2532), "Publisher09", 450 },
                    { 10, 3, new DateTime(2023, 2, 4, 11, 50, 9, 345, DateTimeKind.Local).AddTicks(2543), "Publisher10", 500 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_BookId",
                table: "Publishers",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOrders");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
