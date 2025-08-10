using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        private readonly VarlikZimmetEnginContext _db;

        public RolRepository(VarlikZimmetEnginContext db) : base(db)
        {
            _db = db;
        }


    }
}