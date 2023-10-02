using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class cartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdectCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cartItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemProduct",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false),
                    productsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemProduct", x => new { x.CartItemId, x.productsID });
                    table.ForeignKey(
                        name: "FK_CartItemProduct_cartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "cartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItemProduct_products_productsID",
                        column: x => x.productsID,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemProduct_productsID",
                table: "CartItemProduct",
                column: "productsID");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_UserId",
                table: "cartItems",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemProduct");

            migrationBuilder.DropTable(
                name: "cartItems");
        }
    }
}
