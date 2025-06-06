﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energie.Infrastructure.Migrations
{
    public partial class Add_feedback_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FeedBackDate",
                table: "Feedbacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedBackDate",
                table: "Feedbacks");
        }
    }
}
