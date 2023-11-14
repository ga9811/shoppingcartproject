using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewShoppingCart.Migrations
{
    /// <inheritdoc />
    public partial class Updatelogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModel_Cart_CartId",
                table: "ItemModel");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_ItemModel_CartId",
                table: "ItemModel");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "ItemModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "ItemModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemModel_CartId",
                table: "ItemModel",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModel_Cart_CartId",
                table: "ItemModel",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id");
        }
    }
}
