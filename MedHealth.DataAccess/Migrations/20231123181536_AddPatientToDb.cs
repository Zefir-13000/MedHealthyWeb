using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHealth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialties_SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "AspNetUsers",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SpecialityId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PatientId");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId",
                table: "AspNetUsers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "AspNetUsers",
                newName: "SpecialityId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PatientId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialties_SpecialityId",
                table: "AspNetUsers",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }
    }
}
