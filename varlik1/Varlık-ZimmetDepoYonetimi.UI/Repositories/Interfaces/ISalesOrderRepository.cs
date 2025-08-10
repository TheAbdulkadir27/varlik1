using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface ISalesOrderRepository : IGenericRepository<SalesOrder>
    {
        Task<string> Delete(SalesOrder entity);
    }
}
