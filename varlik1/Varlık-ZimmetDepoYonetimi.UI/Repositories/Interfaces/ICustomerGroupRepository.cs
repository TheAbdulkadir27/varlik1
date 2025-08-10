using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ICustomerGroupRepository : IGenericRepository<CustomerGroup>
    {
        Task<string> Delete(CustomerGroup entity);
    }
}
