using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TabHelper.Migrations
{
    public partial class AddedFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Observation = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tabulations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
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
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTabulations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    TabulationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTabulations", x => new { x.DepartmentId, x.TabulationId });
                    table.UniqueConstraint("AK_DepartmentTabulations_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentTabulations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentTabulations_Tabulations_TabulationId",
                        column: x => x.TabulationId,
                        principalTable: "Tabulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
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
                        principalTable: "TabulationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_Tabulations_TabulationId",
                        column: x => x.TabulationId,
                        principalTable: "Tabulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentTabulations_TabulationId",
                table: "DepartmentTabulations",
                column: "TabulationId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_TabulationAttributesId",
                table: "Forms",
                column: "TabulationAttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabulations_DepartmentId",
                table: "Tabulations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentTabulations");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Historics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TabulationAttributes");

            migrationBuilder.DropTable(
                name: "Tabulations");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
