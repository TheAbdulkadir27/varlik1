using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class PurchaseOrderitemRepository : GenericRepository<PurchaseOrderItem>, IPurchaseOrderitemRepository
    {
        public PurchaseOrderitemRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        async Task<string> IPurchaseOrderitemRepository.Delete(PurchaseOrderItem entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
        public override async Task<PurchaseOrderItem> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Urun).Include(x => x.PurchaseOrder).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async override Task<IEnumerable<PurchaseOrderItem>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Urun).Include(x => x.PurchaseOrder).AsNoTracking().ToListAsync();
        }
    }
}
