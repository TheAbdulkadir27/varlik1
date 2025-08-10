using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        public Task<string?> Delete(Customer entity);
    }
}
