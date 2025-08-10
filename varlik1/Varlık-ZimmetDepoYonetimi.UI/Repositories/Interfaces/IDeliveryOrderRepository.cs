using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IDeliveryOrderRepository : IGenericRepository<DeliveryOrder>
    {
        Task<string> Delete(DeliveryOrder entity);
    }
}
