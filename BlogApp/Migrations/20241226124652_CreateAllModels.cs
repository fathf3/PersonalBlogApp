using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Resumes_ResumeId",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "Responsibilities",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "GitHubLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "GPA",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameColumn(
                name: "Technologies",
                table: "WorkExperiences",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Projects",
                newName: "ProjectUrl");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Projects",
                newName: "GitHubUrl");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Education",
                newName: "Grade");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "Education",
                newName: "InstitutionName");

            migrationBuilder.RenameColumn(
                name: "Field",
                table: "Education",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_ResumeId",
                table: "Education",
                newName: "IX_Education_ResumeId");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "WorkExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyUrl",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProficiencyLevel",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Resumes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "Certificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuingOrganization",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldOfStudy",
                table: "Education",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, null, null, "3208e599-37d0-4f66-a5ab-99d7a5314813", "test@example.com", true, "Test", new DateTime(2024, 12, 26, 15, 46, 52, 375, DateTimeKind.Local).AddTicks(9250), "User", false, null, "TEST@EXAMPLE.COM", "TEST@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDPxFbhRXgrbuWL0WXZW9fxjWhuzwO5lehA/jZeUsqEZnDiADt6KEZaFlxW8A+C3LA==", null, false, "dc18b1c3-672d-412d-8623-97d8e54b3612", false, "test@example.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AuthorId",
                table: "Resumes",
                column: "AuthorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resumes_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AspNetUsers_AuthorId",
                table: "Resumes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resumes_ResumeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AspNetUsers_AuthorId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_AuthorId",
                table: "Resumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DropColumn(
                name: "CompanyUrl",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "ProficiencyLevel",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IssuingOrganization",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "Education");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "WorkExperiences",
                newName: "Technologies");

            migrationBuilder.RenameColumn(
                name: "ProjectUrl",
                table: "Projects",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "GitHubUrl",
                table: "Projects",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "InstitutionName",
                table: "Educations",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Educations",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Activities",
                table: "Educations",
                newName: "Field");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ResumeId",
                table: "Educations",
                newName: "IX_Educations_ResumeId");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "WorkExperiences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Responsibilities",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "GitHubLink",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "Certificates",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Educations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GPA",
                table: "Educations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Resumes_ResumeId",
                table: "Educations",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
