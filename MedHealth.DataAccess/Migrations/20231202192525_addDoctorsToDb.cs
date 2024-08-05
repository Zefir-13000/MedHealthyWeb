using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHealth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDoctorsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctor_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialties_SpecialityId",
                table: "Doctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_SpecialityId",
                table: "Doctors",
                newName: "IX_Doctors_SpecialityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialityId",
                table: "Doctors",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialityId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctor",
                newName: "IX_Doctor_SpecialityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctor_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialties_SpecialityId",
                table: "Doctor",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }
    }
}
