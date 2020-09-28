using Microsoft.EntityFrameworkCore.Migrations;

namespace TissueSampleRecords.Migrations
{
    public partial class CollectionIdAddedToSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Collection_Id",
                table: "Samples",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collection_Id",
                table: "Samples");
        }
    }
}
