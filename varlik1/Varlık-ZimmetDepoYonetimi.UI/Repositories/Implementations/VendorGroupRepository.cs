using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class VendorGroupRepository : GenericRepository<VendorGroup>, IVendorGroupRepository
    {
        public VendorGroupRepository(VarlikZimmetEnginContext context) : base(context)
        {

        }
        async Task<string> IVendorGroupRepository.Delete(VendorGroup entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
