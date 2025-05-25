using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudTrabajoBimestral.API.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Espacio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "varchar(100)", nullable: false),
                    location = table.Column<string>(type: "varchar(255)", nullable: false),
                    maxCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    Cedula = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Lastname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "Ponente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Lastname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: false),
                    Especialidad = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sesion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    horaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    horaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EspacioID = table.Column<int>(type: "integer", nullable: false),
                    EventoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesion_Espacio_EspacioID",
                        column: x => x.EspacioID,
                        principalTable: "Espacio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sesion_Evento_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fechaInscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Cedula = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParticipanteCedula = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripcion_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripcion_Participante_ParticipanteCedula",
                        column: x => x.ParticipanteCedula,
                        principalTable: "Participante",
                        principalColumn: "Cedula");
                });

            migrationBuilder.CreateTable(
                name: "PonenteSesion",
                columns: table => new
                {
                    PonentesId = table.Column<int>(type: "integer", nullable: false),
                    SesionesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PonenteSesion", x => new { x.PonentesId, x.SesionesId });
                    table.ForeignKey(
                        name: "FK_PonenteSesion_Ponente_PonentesId",
                        column: x => x.PonentesId,
                        principalTable: "Ponente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PonenteSesion_Sesion_SesionesId",
                        column: x => x.SesionesId,
                        principalTable: "Sesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionPonente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SesionId = table.Column<int>(type: "integer", nullable: false),
                    PonenteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionPonente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionPonente_Ponente_PonenteId",
                        column: x => x.PonenteId,
                        principalTable: "Ponente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionPonente_Sesion_SesionId",
                        column: x => x.SesionId,
                        principalTable: "Sesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fechaAsistencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    sesionId = table.Column<int>(type: "integer", nullable: false),
                    inscripcionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencia_Inscripcion_inscripcionId",
                        column: x => x.inscripcionId,
                        principalTable: "Inscripcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencia_Sesion_sesionId",
                        column: x => x.sesionId,
                        principalTable: "Sesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UrlDescarga = table.Column<string>(type: "text", nullable: false),
                    InscripcionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificado_Inscripcion_InscripcionID",
                        column: x => x.InscripcionID,
                        principalTable: "Inscripcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    monto = table.Column<double>(type: "double precision", nullable: false),
                    fechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    medioPago = table.Column<string>(type: "varchar(50)", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    InscripcionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Inscripcion_InscripcionID",
                        column: x => x.InscripcionID,
                        principalTable: "Inscripcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_inscripcionId",
                table: "Asistencia",
                column: "inscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_sesionId",
                table: "Asistencia",
                column: "sesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificado_InscripcionID",
                table: "Certificado",
                column: "InscripcionID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripcion_EventoId",
                table: "Inscripcion",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripcion_ParticipanteCedula",
                table: "Inscripcion",
                column: "ParticipanteCedula");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_InscripcionID",
                table: "Pago",
                column: "InscripcionID");

            migrationBuilder.CreateIndex(
                name: "IX_PonenteSesion_SesionesId",
                table: "PonenteSesion",
                column: "SesionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_EspacioID",
                table: "Sesion",
                column: "EspacioID");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_EventoID",
                table: "Sesion",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPonente_PonenteId",
                table: "SesionPonente",
                column: "PonenteId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPonente_SesionId",
                table: "SesionPonente",
                column: "SesionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Certificado");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "PonenteSesion");

            migrationBuilder.DropTable(
                name: "SesionPonente");

            migrationBuilder.DropTable(
                name: "Inscripcion");

            migrationBuilder.DropTable(
                name: "Ponente");

            migrationBuilder.DropTable(
                name: "Sesion");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Espacio");

            migrationBuilder.DropTable(
                name: "Evento");
        }
    }
}
