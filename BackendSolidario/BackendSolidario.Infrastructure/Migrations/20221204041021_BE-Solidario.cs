using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSolidario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BESolidario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    cliid = table.Column<int>(name: "cli_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identificacion = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    nombre = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    cliusuario = table.Column<string>(name: "cli_usuario", type: "varchar(16)", maxLength: 16, nullable: false),
                    clicontrasena = table.Column<string>(name: "cli_contrasena", type: "varchar(500)", maxLength: 500, nullable: false),
                    clisalt = table.Column<string>(name: "cli_salt", type: "varchar(500)", maxLength: 500, nullable: false),
                    cliestado = table.Column<bool>(name: "cli_estado", type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.cliid);
                });

            migrationBuilder.CreateTable(
                name: "cuenta",
                columns: table => new
                {
                    cuenumerocuenta = table.Column<string>(name: "cue_numero_cuenta", type: "varchar(16)", maxLength: 16, nullable: false),
                    cuetipocuenta = table.Column<string>(name: "cue_tipo_cuenta", type: "varchar(16)", maxLength: 16, nullable: false),
                    cuesaldoinicial = table.Column<decimal>(name: "cue_saldo_inicial", type: "decimal(15,3)", nullable: false),
                    cueestado = table.Column<bool>(name: "cue_estado", type: "bit", nullable: false, defaultValue: true),
                    cueclienteId = table.Column<int>(name: "cue_clienteId", type: "int", nullable: false),
                    cueid = table.Column<int>(name: "cue_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuenta", x => x.cuenumerocuenta);
                    table.ForeignKey(
                        name: "FK_cuenta_cliente_cue_clienteId",
                        column: x => x.cueclienteId,
                        principalTable: "cliente",
                        principalColumn: "cli_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movimiento",
                columns: table => new
                {
                    movid = table.Column<int>(name: "mov_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movusuario = table.Column<DateTime>(name: "mov_usuario", type: "datetime", nullable: false),
                    movtipomovimiento = table.Column<string>(name: "mov_tipo_movimiento", type: "varchar(16)", maxLength: 16, nullable: false),
                    movvalor = table.Column<decimal>(name: "mov_valor", type: "decimal(15,3)", nullable: false),
                    movsaldo = table.Column<decimal>(name: "mov_saldo", type: "decimal(15,3)", nullable: false),
                    movcuentaId = table.Column<string>(name: "mov_cuentaId", type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimiento", x => x.movid);
                    table.ForeignKey(
                        name: "FK_movimiento_cuenta_mov_cuentaId",
                        column: x => x.movcuentaId,
                        principalTable: "cuenta",
                        principalColumn: "cue_numero_cuenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cuenta_cue_clienteId",
                table: "cuenta",
                column: "cue_clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_movimiento_mov_cuentaId",
                table: "movimiento",
                column: "mov_cuentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimiento");

            migrationBuilder.DropTable(
                name: "cuenta");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
