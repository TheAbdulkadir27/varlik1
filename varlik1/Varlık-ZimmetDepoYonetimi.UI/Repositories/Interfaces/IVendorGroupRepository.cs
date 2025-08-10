using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IVendorGroupRepository :IGenericRepository<VendorGroup>
    {
        Task<string> Delete(VendorGroup entity);
    }
}
