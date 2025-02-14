using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "History" },
                    { 2, 2, "Action" },
                    { 3, 3, "Latest" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", "A comprehensive guide to C# programming.", "978-1234567890", 49.990000000000002, 45.990000000000002, 35.990000000000002, 40.990000000000002, "C# Programming Guide" },
                    { 2, "Jane Smith", "Learn the essentials of ASP.NET Core.", "978-0987654321", 59.990000000000002, 54.990000000000002, 45.990000000000002, 50.990000000000002, "ASP.NET Core Essentials" },
                    { 3, "Mark Johnson", "Master Entity Framework Core with real-world examples.", "978-1122334455", 39.990000000000002, 35.990000000000002, 25.989999999999998, 30.989999999999998, "Entity Framework Core in Action" },
                    { 4, "Emily Davis", "A deep dive into LINQ with practical examples.", "978-2233445566", 44.990000000000002, 40.990000000000002, 32.990000000000002, 36.990000000000002, "Mastering LINQ" },
                    { 5, "Michael Brown", "Learn how to build interactive web applications using Blazor WebAssembly.", "978-3344556677", 69.989999999999995, 64.989999999999995, 54.990000000000002, 59.990000000000002, "Blazor WebAssembly Guide" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
