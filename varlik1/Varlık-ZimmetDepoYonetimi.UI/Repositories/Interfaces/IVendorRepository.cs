using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IVendorRepository : IGenericRepository<Vendor>
    {
        public Task<string?> Delete(Vendor entity);
    }
   
}
