using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMSystem_GymSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "dbo",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BranchLocation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BranchStatus = table.Column<bool>(type: "bit", nullable: false),
                    WorkStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkEnd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "dbo",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    stillWork = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmployeeNumber = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "dbo",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriptionPrice = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPeriod = table.Column<int>(type: "int", nullable: false),
                    SubscriptionStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                schema: "dbo",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    stillWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trainerSalary = table.Column<int>(type: "int", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trainerNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    trainerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trainerEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriptionSalary = table.Column<int>(type: "int", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.TrainerId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "dbo",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ClientNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pay = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Branchid = table.Column<int>(type: "int", nullable: false),
                    Departmentid = table.Column<int>(type: "int", nullable: false),
                    Trainerid = table.Column<int>(type: "int", nullable: false),
                    Subscriptionid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Client_Branch_Branchid",
                        column: x => x.Branchid,
                        principalSchema: "dbo",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Department_Departmentid",
                        column: x => x.Departmentid,
                        principalSchema: "dbo",
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Subscription_Subscriptionid",
                        column: x => x.Subscriptionid,
                        principalSchema: "dbo",
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Trainer_Trainerid",
                        column: x => x.Trainerid,
                        principalSchema: "dbo",
                        principalTable: "Trainer",
                        principalColumn: "TrainerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Branchid",
                schema: "dbo",
                table: "Client",
                column: "Branchid");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Departmentid",
                schema: "dbo",
                table: "Client",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Subscriptionid",
                schema: "dbo",
                table: "Client",
                column: "Subscriptionid");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Trainerid",
                schema: "dbo",
                table: "Client",
                column: "Trainerid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Trainer",
                schema: "dbo");
        }
    }
}
