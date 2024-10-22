using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Migrations
{
    public partial class UpdateMigration_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__VoiceAudi__recor__282DF8C2",
                table: "VoiceAudio");

            migrationBuilder.AddForeignKey(
                name: "FK__VoiceAudi__recor__282DF8C2",
                table: "VoiceAudio",
                column: "recording_id",
                principalTable: "Recording",
                principalColumn: "recording_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__VoiceAudi__recor__282DF8C2",
                table: "VoiceAudio");

            migrationBuilder.AddForeignKey(
                name: "FK__VoiceAudi__recor__282DF8C2",
                table: "VoiceAudio",
                column: "recording_id",
                principalTable: "Recording",
                principalColumn: "recording_id");
        }
    }
}
