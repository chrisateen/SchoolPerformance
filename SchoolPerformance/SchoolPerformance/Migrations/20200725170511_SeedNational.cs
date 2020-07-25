using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolPerformance.Migrations
{
    public partial class SeedNational : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "URN", "ESTAB", "LA", "SCHNAME" },
                values: new object[] { 9, 0, 0, "" });

            migrationBuilder.InsertData(
                table: "SchoolContextual",
                columns: new[] { "URN", "ACADEMICYEAR", "NOR", "PNORG", "PNUMEAL", "PNUMFSMEVER", "PSENELK", "PSENELSE" },
                values: new object[] { 9, 2019, 3327970, 49.799999999999997, 16.899999999999999, 27.699999999999999, 10.800000000000001, 1.7 });

            migrationBuilder.InsertData(
                table: "SchoolResult",
                columns: new[] { "URN", "ACADEMICYEAR", "ATT8SCR", "ATT8SCR_FSM6CLA1A", "ATT8SCR_NFSM6CLA1A", "P8MEA", "P8MEA_FSM6CLA1A", "P8MEA_NFSM6CLA1A", "PTFSM6CLA1A", "PTFSM6CLA1ABASICS_94", "PTFSM6CLA1ABASICS_95", "PTL2BASICS_94", "PTL2BASICS_95", "PTNOTFSM6CLA1ABASICS_94", "PTNOTFSM6CLA1ABASICS_95" },
                values: new object[] { 9, 2019, 46.700000000000003, 36.700000000000003, 50.299999999999997, -0.029999999999999999, -0.45000000000000001, 0.13, 0.27000000000000002, 0.45000000000000001, 0.25, 0.65000000000000002, 0.42999999999999999, 0.71999999999999997, 0.5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SchoolContextual",
                keyColumns: new[] { "URN", "ACADEMICYEAR" },
                keyValues: new object[] { 9, 2019 });

            migrationBuilder.DeleteData(
                table: "SchoolResult",
                keyColumns: new[] { "URN", "ACADEMICYEAR" },
                keyValues: new object[] { 9, 2019 });

            migrationBuilder.DeleteData(
                table: "School",
                keyColumn: "URN",
                keyValue: 9);
        }
    }
}
