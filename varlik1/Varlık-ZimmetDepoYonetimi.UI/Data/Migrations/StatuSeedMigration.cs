using Microsoft.EntityFrameworkCore.Migrations;

namespace Varlık_ZimmetDepoYonetimi.UI.Data.Migrations
{
    public partial class StatuSeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Önce UrunStatu tablosundaki ilişkili kayıtları temizleyelim
            migrationBuilder.Sql("DELETE FROM UrunStatu");

            // Sonra Statu tablosunu temizleyelim
            migrationBuilder.Sql("DELETE FROM Statu");

            // IDENTITY_INSERT'i açıp statüleri ekleyelim
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Statu ON;
                
                INSERT INTO Statu (StatuId, StatuAdi, AktifMi) VALUES 
                (1, N'Kullanımda', 1),
                (2, N'Arızalı', 1),
                (3, N'Tamirde', 1),
                (4, N'Kullanım Dışı', 1);
                
                SET IDENTITY_INSERT Statu OFF;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM UrunStatu");
            migrationBuilder.Sql("DELETE FROM Statu");
        }
    }
}