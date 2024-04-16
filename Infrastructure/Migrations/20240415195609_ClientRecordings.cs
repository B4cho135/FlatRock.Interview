using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientRecordings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoRecordings_Clients_ClientId",
                schema: "public",
                table: "VideoRecordings");

            migrationBuilder.DropIndex(
                name: "IX_VideoRecordings_ClientId",
                schema: "public",
                table: "VideoRecordings");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "public",
                table: "VideoRecordings");

            migrationBuilder.CreateTable(
                name: "ClientRecordings",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    VideoRecordingId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRecordings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRecordings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "public",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRecordings_VideoRecordings_VideoRecordingId",
                        column: x => x.VideoRecordingId,
                        principalSchema: "public",
                        principalTable: "VideoRecordings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecordings_ClientId",
                schema: "public",
                table: "ClientRecordings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecordings_VideoRecordingId",
                schema: "public",
                table: "ClientRecordings",
                column: "VideoRecordingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRecordings",
                schema: "public");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                schema: "public",
                table: "VideoRecordings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VideoRecordings_ClientId",
                schema: "public",
                table: "VideoRecordings",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoRecordings_Clients_ClientId",
                schema: "public",
                table: "VideoRecordings",
                column: "ClientId",
                principalSchema: "public",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
