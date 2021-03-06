﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolPerformance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    URN = table.Column<int>(nullable: false),
                    LA = table.Column<int>(nullable: false),
                    ESTAB = table.Column<int>(nullable: false),
                    SCHNAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.URN);
                });

            migrationBuilder.CreateTable(
                name: "SchoolContextual",
                columns: table => new
                {
                    URN = table.Column<int>(nullable: false),
                    ACADEMICYEAR = table.Column<int>(nullable: false),
                    NOR = table.Column<int>(nullable: true),
                    PNORG = table.Column<double>(nullable: true),
                    PSENELSE = table.Column<double>(nullable: true),
                    PSENELK = table.Column<double>(nullable: true),
                    PNUMEAL = table.Column<double>(nullable: true),
                    PNUMFSMEVER = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolContextual", x => new { x.URN, x.ACADEMICYEAR });
                    table.ForeignKey(
                        name: "FK_SchoolContextual_School_URN",
                        column: x => x.URN,
                        principalTable: "School",
                        principalColumn: "URN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolDetails",
                columns: table => new
                {
                    URN = table.Column<int>(nullable: false),
                    STREET = table.Column<string>(nullable: true),
                    LOCALITY = table.Column<string>(nullable: true),
                    ADDRESS3 = table.Column<string>(nullable: true),
                    TOWN = table.Column<string>(nullable: true),
                    POSTCODE = table.Column<string>(nullable: true),
                    SCHOOLTYPE = table.Column<string>(nullable: true),
                    GENDER = table.Column<string>(nullable: true),
                    RELCHAR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolDetails", x => x.URN);
                    table.ForeignKey(
                        name: "FK_SchoolDetails_School_URN",
                        column: x => x.URN,
                        principalTable: "School",
                        principalColumn: "URN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolResult",
                columns: table => new
                {
                    URN = table.Column<int>(nullable: false),
                    ACADEMICYEAR = table.Column<int>(nullable: false),
                    PTFSM6CLA1A = table.Column<double>(nullable: true),
                    ATT8SCR = table.Column<double>(nullable: true),
                    ATT8SCR_FSM6CLA1A = table.Column<double>(nullable: true),
                    ATT8SCR_NFSM6CLA1A = table.Column<double>(nullable: true),
                    P8MEA = table.Column<double>(nullable: true),
                    P8MEA_FSM6CLA1A = table.Column<double>(nullable: true),
                    P8MEA_NFSM6CLA1A = table.Column<double>(nullable: true),
                    PTL2BASICS_94 = table.Column<double>(nullable: true),
                    PTFSM6CLA1ABASICS_94 = table.Column<double>(nullable: true),
                    PTNOTFSM6CLA1ABASICS_94 = table.Column<double>(nullable: true),
                    PTL2BASICS_95 = table.Column<double>(nullable: true),
                    PTFSM6CLA1ABASICS_95 = table.Column<double>(nullable: true),
                    PTNOTFSM6CLA1ABASICS_95 = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolResult", x => new { x.URN, x.ACADEMICYEAR });
                    table.ForeignKey(
                        name: "FK_SchoolResult_School_URN",
                        column: x => x.URN,
                        principalTable: "School",
                        principalColumn: "URN",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolContextual");

            migrationBuilder.DropTable(
                name: "SchoolDetails");

            migrationBuilder.DropTable(
                name: "SchoolResult");

            migrationBuilder.DropTable(
                name: "School");
        }
    }
}
