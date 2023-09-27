using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTickets.Migrations
{
    /// <inheritdoc />
    public partial class fixAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrpfilePictureURL",
                table: "Producers",
                newName: "ProfilePictureURL");

            migrationBuilder.RenameColumn(
                name: "PrpfilePictureURL",
                table: "Actors",
                newName: "ProfilePictureURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Producers",
                newName: "PrpfilePictureURL");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Actors",
                newName: "PrpfilePictureURL");
        }
    }
}
