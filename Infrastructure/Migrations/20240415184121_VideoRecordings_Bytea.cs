﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VideoRecordings_Bytea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "VideoRecording",
                schema: "public",
                table: "VideoRecordings",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "blob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "VideoRecording",
                schema: "public",
                table: "VideoRecordings",
                type: "blob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }
    }
}
