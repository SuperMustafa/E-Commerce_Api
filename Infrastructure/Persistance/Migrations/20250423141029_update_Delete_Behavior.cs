﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class update_Delete_Behavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderEntityId",
                table: "OrderItems",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderEntityId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderEntityId",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderEntityId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
