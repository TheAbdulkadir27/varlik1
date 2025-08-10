using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IDeliveryOrderitemRepository : IGenericRepository<DeliveryOrderitem>
    {
        Task<string> Delete(DeliveryOrderitem entity);
    }
}
