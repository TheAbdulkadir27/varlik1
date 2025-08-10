using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class DeliveryOrderRepository : GenericRepository<DeliveryOrder>, IDeliveryOrderRepository
    {
        public DeliveryOrderRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        public override Task<DeliveryOrder> GetByIdAsync(int id)
        {
            return _dbSet.Include(x => x.SalesOrder).ThenInclude(x => x.Customer).Include(x => x.DeliveryOrderItemList).ThenInclude(x=>x.Urun).ThenInclude(x=>x.Model).AsNoTracking().SingleAsync(x => x.ID == id);
        }
        public override async Task<IEnumerable<DeliveryOrder>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.SalesOrder).ThenInclude(x=>x.Customer).Include(x => x.DeliveryOrderItemList).ThenInclude(x => x.Urun).ThenInclude(x => x.Model).AsNoTracking().ToListAsync();
        }
        async Task<string> IDeliveryOrderRepository.Delete(DeliveryOrder entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
