using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "ProductGroup",
                newName: "ProductGroupName");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroup_ProductName",
                table: "ProductGroup",
                newName: "IX_ProductGroup_ProductGroupName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductGroupName",
                table: "ProductGroup",
                newName: "ProductName");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroup_ProductGroupName",
                table: "ProductGroup",
                newName: "IX_ProductGroup_ProductName");
        }
    }
}
