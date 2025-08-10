using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IPurchaseOrderitemRepository : IGenericRepository<PurchaseOrderItem>
    {
        Task<string> Delete(PurchaseOrderItem entity);
    }
}
