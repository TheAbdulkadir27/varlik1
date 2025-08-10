using Microsoft.EntityFrameworkCore.Storage;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        Task<List<Urun>> GetDusukStokluUrunlerAsync(int minimumStok);
        Task<IEnumerable<Urun>> GetAllWithModelAsync();
        Task<IEnumerable<Urun>> GetAllWithDetailsAsync();
        Task<List<UrunStatu>> GetUrunStatuListAsync(int urunId);
        void DeleteUrunStatu(UrunStatu urunStatu);
        Task<List<Zimmet>> GetZimmetListByUrunIdAsync(int urunId);
        void DeleteZimmet(Zimmet zimmet);
        Task<List<DepoUrun>> GetDepoUrunListAsync(int urunId);
        void DeleteDepoUrun(DepoUrun depoUrun);
        void DeleteKayipCalinti(KayipCalinti kayipCalinti);
        void DeleteMusteriZimmet(MusteriZimmet musteriZimmet);
        void DeleteUrunBarkod(UrunBarkod urunBarkod);
        void DeleteUrunDetay(UrunDetay urunDetay);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}