using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IVendorCategoryRepository : IGenericRepository<VendorCategory>
    {
        Task<string> Delete(VendorCategory entity);
    }
}
