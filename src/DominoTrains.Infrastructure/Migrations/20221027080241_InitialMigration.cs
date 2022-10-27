using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DominoTrains.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EdgeValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Train_Dominoes",
                columns: table => new
                {
                    TrainId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    A = table.Column<int>(type: "integer", nullable: false),
                    B = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train_Dominoes", x => new { x.TrainId, x.Id });
                    table.ForeignKey(
                        name: "FK_Train_Dominoes_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainStation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NorthId = table.Column<Guid>(type: "uuid", nullable: false),
                    EastId = table.Column<Guid>(type: "uuid", nullable: false),
                    WestId = table.Column<Guid>(type: "uuid", nullable: false),
                    SouthId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainStation_Train_EastId",
                        column: x => x.EastId,
                        principalTable: "Train",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainStation_Train_NorthId",
                        column: x => x.NorthId,
                        principalTable: "Train",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainStation_Train_SouthId",
                        column: x => x.SouthId,
                        principalTable: "Train",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainStation_Train_WestId",
                        column: x => x.WestId,
                        principalTable: "Train",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainStationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_TrainStation_TrainStationId",
                        column: x => x.TrainStationId,
                        principalTable: "TrainStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games_Dominoes",
                columns: table => new
                {
                    PlayerGameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    A = table.Column<int>(type: "integer", nullable: false),
                    B = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games_Dominoes", x => new { x.PlayerGameId, x.Id });
                    table.ForeignKey(
                        name: "FK_Games_Dominoes_Games_PlayerGameId",
                        column: x => x.PlayerGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TrainStationId",
                table: "Games",
                column: "TrainStationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainStation_EastId",
                table: "TrainStation",
                column: "EastId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainStation_NorthId",
                table: "TrainStation",
                column: "NorthId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainStation_SouthId",
                table: "TrainStation",
                column: "SouthId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainStation_WestId",
                table: "TrainStation",
                column: "WestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games_Dominoes");

            migrationBuilder.DropTable(
                name: "Train_Dominoes");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "TrainStation");

            migrationBuilder.DropTable(
                name: "Train");
        }
    }
}
