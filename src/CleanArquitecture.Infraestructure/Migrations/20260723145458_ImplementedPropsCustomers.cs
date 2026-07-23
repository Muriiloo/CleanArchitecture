using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArquitecture.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ImplementedPropsCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDay",
                table: "customers",
                type: "date",
                maxLength: 10,
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "customers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "customers");
        }
    }
}
