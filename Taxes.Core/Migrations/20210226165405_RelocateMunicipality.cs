using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taxes.Core.Migrations
{
    public partial class RelocateMunicipality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tax_Municipality",
                schema: "dbo",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "Municipality",
                schema: "dbo",
                table: "Tax");

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityId",
                schema: "dbo",
                table: "Tax",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Municipality",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tax_MunicipalityId",
                schema: "dbo",
                table: "Tax",
                column: "MunicipalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipality",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Tax_MunicipalityId",
                schema: "dbo",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                schema: "dbo",
                table: "Tax");

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                schema: "dbo",
                table: "Tax",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tax_Municipality",
                schema: "dbo",
                table: "Tax",
                column: "Municipality");
        }
    }
}
