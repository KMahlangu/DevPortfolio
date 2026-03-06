using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHubUrl", "ImageUrl", "LiveUrl", "Technologies", "Title" },
                values: new object[,]
                {
                    { 1, "<y personal portfolio built with ASP.NET Core MVC.", "https://github.com/KMahlangu/DevPortfolio", null, null, "C#, ASP.NET Core, Bootstrap", "Portfolio Website" },
                    { 2, "A simple task management application", null, null, "https://example.com", "JavaScript, Node.js, MongoDB", "Task Manager App" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "DisplayOrder", "IconClass", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, "Custom web applications built with ASP.NET Core", 0, "bi-code-slash", true, "Web Development" },
                    { 2, "Effecient database architecture and optimization", 0, "bi-database", true, "Database Design" },
                    { 3, "RESTful APIs for seamless integration", 0, "bi-plugin", true, "API Development" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Level", "Name" },
                values: new object[,]
                {
                    { 1, "Backend", 85, "C#" },
                    { 2, "Froentend", 70, "Javascript" },
                    { 3, "Database", 60, "SQL" },
                    { 4, "Backend", 75, "ASP.Net Core" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
