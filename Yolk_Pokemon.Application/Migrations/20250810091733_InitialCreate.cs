using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Yolk_Pokemon.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Elements_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Wins = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Losses = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Trainers_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Moves_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Moves_TypeId_fkey",
                        column: x => x.TypeId,
                        principalTable: "Elements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: true),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CaughtAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pokemon_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Pokemon_OwnerId_fkey",
                        column: x => x.OwnerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Pokemon_TypeId_fkey",
                        column: x => x.TypeId,
                        principalTable: "Elements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PokemonMoves_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "PokemonMoves_MoveId_fkey",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "PokemonMoves_PokemonId_fkey",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Elements_Name_key",
                table: "Elements",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moves_TypeId",
                table: "Moves",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_OwnerId",
                table: "Pokemon",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TypeId",
                table: "Pokemon",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_MoveId",
                table: "PokemonMoves",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "unique_pokemon_move",
                table: "PokemonMoves",
                columns: new[] { "PokemonId", "MoveId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonMoves");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
