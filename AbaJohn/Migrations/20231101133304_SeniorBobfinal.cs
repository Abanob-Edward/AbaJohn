using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class SeniorBobfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_products_productID",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_productID",
                table: "items",
                newName: "IX_items_productID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_items_products_productID",
                table: "items",
                column: "productID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_products_productID",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_items_productID",
                table: "Item",
                newName: "IX_Item_productID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_products_productID",
                table: "Item",
                column: "productID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
