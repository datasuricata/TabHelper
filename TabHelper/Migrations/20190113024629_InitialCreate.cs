using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TabHelper.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Historics",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    TabulationId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    FormJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabulationAttributes",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ComponentType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabulationAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabulations",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Observation = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    DepartmentId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tabulations_Departments_DepartmentId1",
                        column: x => x.DepartmentId1,
                        principalSchema: "core",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tabulations_Departments_Id",
                        column: x => x.Id,
                        principalSchema: "core",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    UserAccess = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "core",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<short>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    TabulationId = table.Column<int>(nullable: false),
                    TabulationAttributesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => new { x.TabulationId, x.TabulationAttributesId });
                    table.UniqueConstraint("AK_Forms_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_TabulationAttributes_TabulationAttributesId",
                        column: x => x.TabulationAttributesId,
                        principalSchema: "core",
                        principalTable: "TabulationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_Tabulations_TabulationId",
                        column: x => x.TabulationId,
                        principalSchema: "core",
                        principalTable: "Tabulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_TabulationAttributesId",
                schema: "core",
                table: "Forms",
                column: "TabulationAttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabulations_DepartmentId1",
                schema: "core",
                table: "Tabulations",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                schema: "core",
                table: "Users",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forms",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Historics",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "core");

            migrationBuilder.DropTable(
                name: "TabulationAttributes",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Tabulations",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "core");
        }
    }
}
