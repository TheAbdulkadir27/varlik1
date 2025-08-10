using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ISalesOrderItemRepository : IGenericRepository<SalesOrderItem>
    {
        Task<string> Delete(SalesOrderItem entity);
    }
}
