using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplyGraduate.Data.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterBeginningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterEndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GownSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GownSize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Program_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    GownRequest = table.Column<bool>(type: "bit", nullable: false),
                    GownSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DidPaid = table.Column<bool>(type: "bit", nullable: false),
                    DidTake = table.Column<bool>(type: "bit", nullable: false),
                    CompanionStatus = table.Column<bool>(type: "bit", nullable: false),
                    DidJoin = table.Column<bool>(type: "bit", nullable: false),
                    HaveApplied = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HesCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DidJoin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dates",
                columns: new[] { "Id", "CreatedDate", "GraduationDate", "IsDeleted", "ModifiedDate", "RegisterBeginningDate", "RegisterEndingDate" },
                values: new object[] { 1, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(9027), new DateTime(2022, 3, 31, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(9010), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(9028), new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(9009), new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(9009) });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4654), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4654), "Hukuk" },
                    { 2, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4657), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4657), "Mühendislik" },
                    { 3, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4659), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4659), "Tıp Fakültesi" }
                });

            migrationBuilder.InsertData(
                table: "GownSize",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedDate", "Size" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2882), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2883), "XS" },
                    { 2, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2885), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2885), "S" },
                    { 3, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2887), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2887), "M" },
                    { 4, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2888), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2889), "L" },
                    { 5, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2890), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2891), "XL" },
                    { 6, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2892), false, new DateTime(2022, 3, 21, 15, 18, 48, 216, DateTimeKind.Local).AddTicks(2892), "XXL" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CreatedDate", "FacultyId", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6161), 1, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6161), "Hukuk" },
                    { 2, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6163), 2, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6164), "Bilgisayar Mühendisliği" },
                    { 3, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6165), 2, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6166), "Makine Mühendisliği" },
                    { 4, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6167), 2, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6168), "İnşaat Mühendisliği" },
                    { 5, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6169), 3, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(6169), "Tıp" }
                });

            migrationBuilder.InsertData(
                table: "Program",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedDate", "Name", "UnitId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7649), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7649), "Hukuk", 1 },
                    { 2, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7651), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7652), "Bilgisayar Mühendisliği", 2 },
                    { 3, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7653), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7653), "Makine Mühendisliği", 3 },
                    { 4, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7655), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7656), "İnşaat Mühendisliği", 4 },
                    { 5, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7657), false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(7657), "Tıp", 5 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CompanionStatus", "CreatedDate", "DidJoin", "DidPaid", "DidTake", "GownRequest", "GownSize", "HaveApplied", "IsDeleted", "ModifiedDate", "Name", "Note", "ProgramId", "Surname" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2124), false, false, false, false, null, false, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2124), "Mert", null, 2, "Aslan" },
                    { 2, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2128), false, false, false, false, null, false, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2128), "Recep", null, 1, "Yayla" },
                    { 3, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2130), false, false, false, false, null, false, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2131), "Cihan", null, 1, "Eyuboglu" },
                    { 4, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2133), false, false, false, false, null, false, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2133), "Berk", null, 3, "Kayatepe" },
                    { 5, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2135), false, false, false, false, null, false, false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(2136), "Muhammed", null, 5, "Cınıslı" }
                });

            migrationBuilder.InsertData(
                table: "Companions",
                columns: new[] { "Id", "CreatedDate", "DidJoin", "HesCode", "IsDeleted", "ModifiedDate", "Name", "PhoneNumber", "StudentId", "Surname" },
                values: new object[] { 1, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4051), false, "A1B2C3D4F5", false, new DateTime(2022, 3, 21, 15, 18, 48, 215, DateTimeKind.Local).AddTicks(4052), "Samet", "5424173626", 1, "Aslan" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companions_StudentId",
                table: "Companions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_UnitId",
                table: "Program",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_FacultyId",
                table: "Units",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Companions");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "GownSize");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
