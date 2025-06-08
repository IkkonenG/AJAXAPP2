using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJAXAPP.Migrations
{
    /// <inheritdoc />
    public partial class RemovOnlineCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
table: "CITIES");

           
            migrationBuilder.AddForeignKey(
           name: "FK_Cities_Countries_CountryId",
            table: "CITIES",
            column: "CountryId",
            principalTable: "Countries",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
            name: "FK_Cities_Countries_Countryid",
            table: "CITIES");

migrationBuilder.AddForeignKey(
name: "FK_Citties_Countries_CountryId",
table: "CITIES",
column: "CountryId",
principalTable: "Countries",
principalColumn: "Id",
onDelete:
ReferentialAction.Cascade);
        }
    }
}
