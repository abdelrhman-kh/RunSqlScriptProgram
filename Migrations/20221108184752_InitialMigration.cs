using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlScript.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Connections",
                schema: "dbo",
                columns: table => new
                {
                    ConnectionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionsName = table.Column<string>(type: "varchar(150)", nullable: false),
                    ConnectionsAbbr = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionsId);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionString",
                schema: "dbo",
                columns: table => new
                {
                    ConnectionStringID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionStringName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConnectionStringDataSource = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConnectionStringUserID = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConnectionStringPassword = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConnectionStringInitialCatalog = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConnectionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionString", x => x.ConnectionStringID);
                    table.ForeignKey(
                        name: "FK_ConnectionString_Connections_ConnectionsId",
                        column: x => x.ConnectionsId,
                        principalSchema: "dbo",
                        principalTable: "Connections",
                        principalColumn: "ConnectionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionString_ConnectionsId",
                schema: "dbo",
                table: "ConnectionString",
                column: "ConnectionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionString",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Connections",
                schema: "dbo");
        }
    }
}
