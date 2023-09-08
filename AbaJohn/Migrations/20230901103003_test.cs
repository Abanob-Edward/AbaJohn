using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_products_Product_id",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "productImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_Product_id",
                table: "productImages",
                newName: "IX_productImages_Product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productImages",
                table: "productImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productImages_products_Product_id",
                table: "productImages",
                column: "Product_id",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImages_products_Product_id",
                table: "productImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productImages",
                table: "productImages");

            migrationBuilder.RenameTable(
                name: "productImages",
                newName: "ProductImage");

            migrationBuilder.RenameIndex(
                name: "IX_productImages_Product_id",
                table: "ProductImage",
                newName: "IX_ProductImage_Product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_products_Product_id",
                table: "ProductImage",
                column: "Product_id",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
