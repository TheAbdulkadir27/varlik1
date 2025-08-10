using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class VendorCategoryRepository : GenericRepository<VendorCategory>, IVendorCategoryRepository
    {
        public VendorCategoryRepository(VarlikZimmetEnginContext context) : base(context)
        {

        }
        async Task<string> IVendorCategoryRepository.Delete(VendorCategory entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }

}
