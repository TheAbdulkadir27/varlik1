using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseRepository
    {
        public PurchaseOrderRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        public override async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Vendor).Include(x => x.Tax).Include(x => x.PurchaseOrderItemList).AsNoTracking().SingleAsync(x => x.Id == id);
        }
        public async override Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Vendor).Include(x => x.Tax).Include(x => x.PurchaseOrderItemList).AsNoTracking().ToListAsync();
        }
        async Task<string> IPurchaseRepository.Delete(PurchaseOrder entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
