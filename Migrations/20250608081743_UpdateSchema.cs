using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJAXAPP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Cuntries_CountryId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuntries",
                table: "Cuntries");

            migrationBuilder.RenameTable(
                name: "Cuntries",
                newName: "Countries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Cuntries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuntries",
                table: "Cuntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Cuntries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Cuntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
