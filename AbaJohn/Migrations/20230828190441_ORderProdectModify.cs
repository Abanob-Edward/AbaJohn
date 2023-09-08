using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class ORderProdectModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_ProductID",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ProductID",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "orders");

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    ordersId = table.Column<int>(type: "int", nullable: false),
                    productsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.ordersId, x.productsID });
                    table.ForeignKey(
                        name: "FK_OrderProduct_orders_ordersId",
                        column: x => x.ordersId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_products_productsID",
                        column: x => x.productsID,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_productsID",
                table: "OrderProduct",
                column: "productsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_ProductID",
                table: "orders",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_ProductID",
                table: "orders",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
