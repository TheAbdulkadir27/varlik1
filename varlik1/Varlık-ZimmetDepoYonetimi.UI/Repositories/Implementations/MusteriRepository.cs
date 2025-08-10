using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class MusteriRepository : GenericRepository<Musteri>, IMusteriRepository
    {
        private readonly VarlikZimmetEnginContext _db;

        public MusteriRepository(VarlikZimmetEnginContext db) : base(db)
        {
            _db = db;
        }

        // Ekstra metotlar eklenebilir
    }
}