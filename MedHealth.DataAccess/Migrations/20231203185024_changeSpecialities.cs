using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHealth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeSpecialities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialityId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.RenameTable(
                name: "Specialties",
                newName: "Specialities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities");

            migrationBuilder.RenameTable(
                name: "Specialities",
                newName: "Specialties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialityId",
                table: "Doctors",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }
    }
}
