using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ICustomerContactRepository : IGenericRepository<CustomerContact>
    {
        Task<string> Delete(CustomerContact entity);
    }
}
