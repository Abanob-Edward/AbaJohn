using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class setnullableImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nationality",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nationality",
                table: "AspNetUsers");
        }
    }
}
