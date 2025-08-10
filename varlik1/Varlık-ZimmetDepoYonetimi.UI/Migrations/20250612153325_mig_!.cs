using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Varlık_ZimmetDepoYonetimi.UI.Migrations
{
    /// <inheritdoc />
    public partial class mig_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategory", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroup", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModelAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UstModelID = table.Column<int>(type: "int", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Model_Model",
                        column: x => x.UstModelID,
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    MusteriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdSoyad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.MusteriID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RolAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sayfa",
                columns: table => new
                {
                    SayfaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SayfaPath = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    SayfaAd = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sayfa", x => x.SayfaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sirket",
                columns: table => new
                {
                    SirketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SirketAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sirket", x => x.SirketID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Statu",
                columns: table => new
                {
                    StatuID = table.Column<int>(type: "int", nullable: false),
                    StatuAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statu", x => x.StatuID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Percentage = table.Column<double>(type: "double", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCategories", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorGroups", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Yetki",
                columns: table => new
                {
                    YetkiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    YetkiAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetki", x => x.YetkiID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "YetkiTalepleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Yetki = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aciklama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    TalepTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YetkiTalepleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YetkiTalepleri_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FaxNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerGroupId = table.Column<int>(type: "int", nullable: false),
                    CustomerCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerCategory_CustomerCategoryId",
                        column: x => x.CustomerCategoryId,
                        principalTable: "CustomerCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerGroup_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Depo",
                columns: table => new
                {
                    DepoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepoAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SirketID = table.Column<int>(type: "int", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depo", x => x.DepoID);
                    table.ForeignKey(
                        name: "FK_Depo_Sirket",
                        column: x => x.SirketID,
                        principalTable: "Sirket",
                        principalColumn: "SirketID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ekip",
                columns: table => new
                {
                    EkipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EkipAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    SirketID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekip", x => x.EkipID);
                    table.ForeignKey(
                        name: "FK_Ekip_Sirket",
                        column: x => x.SirketID,
                        principalTable: "Sirket",
                        principalColumn: "SirketID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FaxNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendorGroupId = table.Column<int>(type: "int", nullable: false),
                    VendorCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorCategories_VendorCategoryId",
                        column: x => x.VendorCategoryId,
                        principalTable: "VendorCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorGroups_VendorGroupId",
                        column: x => x.VendorGroupId,
                        principalTable: "VendorGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SayfaYetki",
                columns: table => new
                {
                    SayfaID = table.Column<int>(type: "int", nullable: false),
                    YetkiID = table.Column<int>(type: "int", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayfaYetki", x => new { x.SayfaID, x.YetkiID, x.RolID });
                    table.ForeignKey(
                        name: "FK_SayfaYetki_Rol",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SayfaYetki_Sayfa",
                        column: x => x.SayfaID,
                        principalTable: "Sayfa",
                        principalColumn: "SayfaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SayfaYetki_Yetki",
                        column: x => x.YetkiID,
                        principalTable: "Yetki",
                        principalColumn: "YetkiID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    BeforeTaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    AfterTaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesOrder_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calisan",
                columns: table => new
                {
                    CalisanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdSoyad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mail = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RolID = table.Column<int>(type: "int", nullable: true),
                    EkipID = table.Column<int>(type: "int", nullable: true),
                    AboneNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    KullaniciAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sifre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SirketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisan", x => x.CalisanID);
                    table.ForeignKey(
                        name: "FK_Calisan_Ekip",
                        column: x => x.EkipID,
                        principalTable: "Ekip",
                        principalColumn: "EkipID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calisan_Rol",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calisan_Sirket_SirketId",
                        column: x => x.SirketId,
                        principalTable: "Sirket",
                        principalColumn: "SirketID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SirketEkip",
                columns: table => new
                {
                    SirketID = table.Column<int>(type: "int", nullable: false),
                    EkipID = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SirketEkip", x => new { x.SirketID, x.EkipID });
                    table.ForeignKey(
                        name: "FK_SirketEkip_Ekip",
                        column: x => x.EkipID,
                        principalTable: "Ekip",
                        principalColumn: "EkipID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SirketEkip_Sirket",
                        column: x => x.SirketID,
                        principalTable: "Sirket",
                        principalColumn: "SirketID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PurchaseStatus = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendorId = table.Column<int>(type: "int", nullable: true),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    BeforeTaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    AfterTaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorContacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VendorContacts_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeliveryOrders_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryOrderitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeliveryOrderId = table.Column<int>(type: "int", nullable: true),
                    UrunId = table.Column<int>(type: "int", nullable: true),
                    DepoID = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Quantity = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrderitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrderitems_DeliveryOrders_DeliveryOrderId",
                        column: x => x.DeliveryOrderId,
                        principalTable: "DeliveryOrders",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DeliveryOrderitems_Depo_DepoID",
                        column: x => x.DepoID,
                        principalTable: "Depo",
                        principalColumn: "DepoID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DepoUrun",
                columns: table => new
                {
                    DepoUrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepoID = table.Column<int>(type: "int", nullable: true),
                    UrunID = table.Column<int>(type: "int", nullable: true),
                    Miktar = table.Column<short>(type: "smallint", nullable: true),
                    Birim = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepoUrun", x => x.DepoUrunID);
                    table.ForeignKey(
                        name: "FK_DepoUrun_Depo",
                        column: x => x.DepoID,
                        principalTable: "Depo",
                        principalColumn: "DepoID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KayipCalinti",
                columns: table => new
                {
                    KayipCalintiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZimmetId = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KayipMi = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KayipCalinti", x => x.KayipCalintiID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusteriZimmet",
                columns: table => new
                {
                    MusteriZimmetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZimmetID = table.Column<int>(type: "int", nullable: false),
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    TuketmeTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriZimmet_1", x => x.MusteriZimmetID);
                    table.ForeignKey(
                        name: "FK_MusteriZimmet_Musteri",
                        column: x => x.MusteriID,
                        principalTable: "Musteri",
                        principalColumn: "MusteriID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusteriZimmetIptalIade",
                columns: table => new
                {
                    MusteriZimmetİptalIadeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MusteriZimmetID = table.Column<int>(type: "int", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", maxLength: 50, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IadeMi = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriZimmetIptalIade", x => x.MusteriZimmetİptalIadeID);
                    table.ForeignKey(
                        name: "FK_MusteriZimmetIptalIade_MusteriZimmet",
                        column: x => x.MusteriZimmetID,
                        principalTable: "MusteriZimmet",
                        principalColumn: "MusteriZimmetID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    UrunId = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Quantity = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesOrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesOrderId = table.Column<int>(type: "int", nullable: true),
                    UrunId = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Quantity = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderItem_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StokHareket",
                columns: table => new
                {
                    StokHareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    IslemTipi = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    Kullanici = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareket", x => x.StokHareketId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Urun",
                columns: table => new
                {
                    UrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    GarantiliMi = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    UrunMaliyeti = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BarkodluMu = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    UrunGuncelFiyat = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    StokMiktari = table.Column<int>(type: "int", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    ProductGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urun", x => x.UrunID);
                    table.ForeignKey(
                        name: "FK_Urun_Model",
                        column: x => x.ModelID,
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Urun_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Urun_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UrunBarkod",
                columns: table => new
                {
                    UrunBarkodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BarkodNo = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UrunID = table.Column<int>(type: "int", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunBarkod", x => x.UrunBarkodID);
                    table.ForeignKey(
                        name: "FK_UrunBarkod_Urun",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UrunDetay",
                columns: table => new
                {
                    UrunDetayID = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: true),
                    Miktar = table.Column<short>(type: "smallint", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunDetay", x => x.UrunDetayID);
                    table.ForeignKey(
                        name: "FK_UrunDetay_Urun",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UrunStatu",
                columns: table => new
                {
                    UrunStatuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    StatuID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime", nullable: true),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunStatu_1", x => x.UrunStatuID);
                    table.ForeignKey(
                        name: "FK_UrunStatu_Statu",
                        column: x => x.StatuID,
                        principalTable: "Statu",
                        principalColumn: "StatuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UrunStatu_Urun",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Zimmet",
                columns: table => new
                {
                    ZimmetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZimmetNumarasi = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrunID = table.Column<int>(type: "int", nullable: true),
                    AtananCalisanId = table.Column<int>(type: "int", nullable: true),
                    AtayanCalisanID = table.Column<int>(type: "int", nullable: true),
                    ZimmetBaslangicTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    ZimmetBitisTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AktifMi = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    ZimmetTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IadeTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CalisanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zimmet", x => x.ZimmetID);
                    table.ForeignKey(
                        name: "FK_Zimmet_Calisan1",
                        column: x => x.AtayanCalisanID,
                        principalTable: "Calisan",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zimmet_Calisan_AtananCalisanId",
                        column: x => x.AtananCalisanId,
                        principalTable: "Calisan",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zimmet_Calisan_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisan",
                        principalColumn: "CalisanID");
                    table.ForeignKey(
                        name: "FK_Zimmet_Urun",
                        column: x => x.UrunID,
                        principalTable: "Urun",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calisan_EkipID",
                table: "Calisan",
                column: "EkipID");

            migrationBuilder.CreateIndex(
                name: "IX_Calisan_RolID",
                table: "Calisan",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Calisan_SirketId",
                table: "Calisan",
                column: "SirketId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerCategoryId",
                table: "Customer",
                column: "CustomerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerGroupId",
                table: "Customer",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrderitems_DeliveryOrderId",
                table: "DeliveryOrderitems",
                column: "DeliveryOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrderitems_DepoID",
                table: "DeliveryOrderitems",
                column: "DepoID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrderitems_UrunId",
                table: "DeliveryOrderitems",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_SalesOrderId",
                table: "DeliveryOrders",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Depo_SirketID",
                table: "Depo",
                column: "SirketID");

            migrationBuilder.CreateIndex(
                name: "IX_DepoUrun_DepoID",
                table: "DepoUrun",
                column: "DepoID");

            migrationBuilder.CreateIndex(
                name: "IX_DepoUrun_UrunID",
                table: "DepoUrun",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_Ekip_SirketID",
                table: "Ekip",
                column: "SirketID");

            migrationBuilder.CreateIndex(
                name: "IX_KayipCalinti_UrunID",
                table: "KayipCalinti",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_KayipCalinti_ZimmetId",
                table: "KayipCalinti",
                column: "ZimmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Model_UstModelID",
                table: "Model",
                column: "UstModelID");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriZimmet_MusteriID",
                table: "MusteriZimmet",
                column: "MusteriID");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriZimmet_ZimmetID",
                table: "MusteriZimmet",
                column: "ZimmetID");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriZimmetIptalIade_MusteriZimmetID",
                table: "MusteriZimmetIptalIade",
                column: "MusteriZimmetID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_UrunId",
                table: "ProductGroup",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_UrunId",
                table: "PurchaseOrderItems",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_TaxId",
                table: "PurchaseOrders",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VendorId",
                table: "PurchaseOrders",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_TaxId",
                table: "SalesOrder",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_SalesOrderId",
                table: "SalesOrderItem",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_UrunId",
                table: "SalesOrderItem",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_SayfaYetki_RolID",
                table: "SayfaYetki",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_SayfaYetki_YetkiID",
                table: "SayfaYetki",
                column: "YetkiID");

            migrationBuilder.CreateIndex(
                name: "IX_SirketEkip_EkipID",
                table: "SirketEkip",
                column: "EkipID");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareket_UrunId",
                table: "StokHareket",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_UrunId",
                table: "UnitMeasures",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Urun_ModelID",
                table: "Urun",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Urun_ProductGroupId",
                table: "Urun",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Urun_UnitMeasureId",
                table: "Urun",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunBarkod_UrunID",
                table: "UrunBarkod",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_UrunDetay_UrunID",
                table: "UrunDetay",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_UrunStatu_StatuID",
                table: "UrunStatu",
                column: "StatuID");

            migrationBuilder.CreateIndex(
                name: "IX_UrunStatu_UrunID",
                table: "UrunStatu",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorCategoryId",
                table: "Vendor",
                column: "VendorCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorGroupId",
                table: "Vendor",
                column: "VendorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorContacts_VendorId",
                table: "VendorContacts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_YetkiTalepleri_UserId",
                table: "YetkiTalepleri",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_AtananCalisanId",
                table: "Zimmet",
                column: "AtananCalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_AtayanCalisanID",
                table: "Zimmet",
                column: "AtayanCalisanID");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_CalisanId",
                table: "Zimmet",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_UrunID",
                table: "Zimmet",
                column: "UrunID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryOrderitems_Urun_UrunId",
                table: "DeliveryOrderitems",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepoUrun_Urun",
                table: "DepoUrun",
                column: "UrunID",
                principalTable: "Urun",
                principalColumn: "UrunID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KayipCalinti_Urun",
                table: "KayipCalinti",
                column: "UrunID",
                principalTable: "Urun",
                principalColumn: "UrunID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KayipCalinti_Zimmet_ZimmetId",
                table: "KayipCalinti",
                column: "ZimmetId",
                principalTable: "Zimmet",
                principalColumn: "ZimmetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriZimmet_Zimmet",
                table: "MusteriZimmet",
                column: "ZimmetID",
                principalTable: "Zimmet",
                principalColumn: "ZimmetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_Urun_UrunId",
                table: "ProductGroup",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_Urun_UrunId",
                table: "PurchaseOrderItems",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItem_Urun_UrunId",
                table: "SalesOrderItem",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID");

            migrationBuilder.AddForeignKey(
                name: "FK_StokHareket_Urun_UrunId",
                table: "StokHareket",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_Urun_UrunId",
                table: "UnitMeasures",
                column: "UrunId",
                principalTable: "Urun",
                principalColumn: "UrunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_Urun_UrunId",
                table: "ProductGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_Urun_UrunId",
                table: "UnitMeasures");

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
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "DeliveryOrderitems");

            migrationBuilder.DropTable(
                name: "DepoUrun");

            migrationBuilder.DropTable(
                name: "KayipCalinti");

            migrationBuilder.DropTable(
                name: "MusteriZimmetIptalIade");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "SalesOrderItem");

            migrationBuilder.DropTable(
                name: "SayfaYetki");

            migrationBuilder.DropTable(
                name: "SirketEkip");

            migrationBuilder.DropTable(
                name: "StokHareket");

            migrationBuilder.DropTable(
                name: "UrunBarkod");

            migrationBuilder.DropTable(
                name: "UrunDetay");

            migrationBuilder.DropTable(
                name: "UrunStatu");

            migrationBuilder.DropTable(
                name: "VendorContacts");

            migrationBuilder.DropTable(
                name: "YetkiTalepleri");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DeliveryOrders");

            migrationBuilder.DropTable(
                name: "Depo");

            migrationBuilder.DropTable(
                name: "MusteriZimmet");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Sayfa");

            migrationBuilder.DropTable(
                name: "Yetki");

            migrationBuilder.DropTable(
                name: "Statu");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "Zimmet");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Calisan");

            migrationBuilder.DropTable(
                name: "VendorCategories");

            migrationBuilder.DropTable(
                name: "VendorGroups");

            migrationBuilder.DropTable(
                name: "CustomerCategory");

            migrationBuilder.DropTable(
                name: "CustomerGroup");

            migrationBuilder.DropTable(
                name: "Ekip");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Sirket");

            migrationBuilder.DropTable(
                name: "Urun");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "UnitMeasures");
        }
    }
}
