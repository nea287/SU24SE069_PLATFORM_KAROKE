using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "Song",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "Song",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150);
        }
    }
}
