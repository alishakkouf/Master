using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteIsDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDoctor",
                table: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDoctor",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
