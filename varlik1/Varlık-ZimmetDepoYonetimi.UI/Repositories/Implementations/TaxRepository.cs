using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class TaxRepository : GenericRepository<Tax>, ITaxRepository
    {
        public TaxRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        async Task<string> ITaxRepository.Delete(Tax entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
    }
}
