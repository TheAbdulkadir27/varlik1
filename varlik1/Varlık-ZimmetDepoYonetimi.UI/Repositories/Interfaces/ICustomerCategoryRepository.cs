using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ICustomerCategoryRepository : IGenericRepository<CustomerCategory>
    {
        public Task<string?> Delete(CustomerCategory entity);
    }
}
