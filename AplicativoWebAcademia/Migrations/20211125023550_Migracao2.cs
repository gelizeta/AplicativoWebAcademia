using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicativoWebAcademia.Migrations
{
    public partial class Migracao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Alterar",
                table: "PessoaModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alterar",
                table: "PessoaModel");
        }
    }
}
