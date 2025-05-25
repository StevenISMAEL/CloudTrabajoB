using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudTrabajoBimestral.API.Migrations
{
    /// <inheritdoc />
    public partial class v03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SesionPonente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    PonenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionPonente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionPonente_Ponente_PonenteId",
                        column: x => x.PonenteId,
                        principalTable: "Ponente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionPonente_Sesion_SesionId",
                        column: x => x.SesionId,
                        principalTable: "Sesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SesionPonente_PonenteId",
                table: "SesionPonente",
                column: "PonenteId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPonente_SesionId",
                table: "SesionPonente",
                column: "SesionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SesionPonente");
        }
    }
}
