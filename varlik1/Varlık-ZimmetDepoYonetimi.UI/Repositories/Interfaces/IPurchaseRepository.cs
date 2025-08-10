using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IPurchaseRepository : IGenericRepository<PurchaseOrder> 
    {
        Task<string> Delete(PurchaseOrder entity);
    }
}
