using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAppointment.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnPhoneNAme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Patients",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_phone",
                table: "Patients",
                newName: "IX_Patients_Phone");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Floor",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Departments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patients",
                newName: "phone");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_Phone",
                table: "Patients",
                newName: "IX_Patients_phone");

            migrationBuilder.AlterColumn<int>(
                name: "Floor",
                table: "Departments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
