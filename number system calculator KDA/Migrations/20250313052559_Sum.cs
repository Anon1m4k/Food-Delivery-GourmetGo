using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace number_system_calculator_KDA.Migrations
{
    /// <inheritdoc />
    public partial class Sum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Sum",
                table: "Dishs",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Dishs");
        }
    }
}
