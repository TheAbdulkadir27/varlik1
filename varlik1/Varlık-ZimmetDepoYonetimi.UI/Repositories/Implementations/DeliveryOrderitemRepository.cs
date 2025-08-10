using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class DeliveryOrderitemRepository : GenericRepository<DeliveryOrderitem>, IDeliveryOrderitemRepository
    {
        public DeliveryOrderitemRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<DeliveryOrderitem>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Depo).ThenInclude(x => x.DepoUruns).Include(x => x.Depo).Include(x => x.DeliveryOrder).AsNoTracking().ToListAsync();
        }
        public override async Task<DeliveryOrderitem> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Depo).ThenInclude(x => x.DepoUruns).Include(x => x.Depo).Include(x => x.DeliveryOrder).AsNoTracking().SingleAsync(x => x.Id == id);
        }

        async Task<string> IDeliveryOrderitemRepository.Delete(DeliveryOrderitem entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
