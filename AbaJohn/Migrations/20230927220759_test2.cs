using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "img",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue :"",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true

                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
              name: "img",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"

              );
        }
    }
}
