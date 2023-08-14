using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energie.Infrastructure.Migrations
{
    public partial class analysisquestion_imageurl_allow_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentFavouriteTipId",
                table: "LikeTip",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "EnergyAnalysisQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTip_DepartmentFavouriteTipId",
                table: "LikeTip",
                column: "DepartmentFavouriteTipId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeTip_DepartmentFavouriteTip_DepartmentFavouriteTipId",
                table: "LikeTip",
                column: "DepartmentFavouriteTipId",
                principalTable: "DepartmentFavouriteTip",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeTip_DepartmentFavouriteTip_DepartmentFavouriteTipId",
                table: "LikeTip");

            migrationBuilder.DropIndex(
                name: "IX_LikeTip_DepartmentFavouriteTipId",
                table: "LikeTip");

            migrationBuilder.DropColumn(
                name: "DepartmentFavouriteTipId",
                table: "LikeTip");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "EnergyAnalysisQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
