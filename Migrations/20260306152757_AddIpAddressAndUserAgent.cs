using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddIpAddressAndUserAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "ContactMessages",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "ContactMessages");
        }
    }
}
