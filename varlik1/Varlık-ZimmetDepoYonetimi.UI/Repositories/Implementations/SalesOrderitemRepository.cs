using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class SalesOrderitemRepository : GenericRepository<SalesOrderItem>, ISalesOrderItemRepository
    {
        public SalesOrderitemRepository(VarlikZimmetEnginContext context) : base(context)
        {

        }
        public override async Task<SalesOrderItem> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Urun).Include(x => x.SalesOrder).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async override Task<IEnumerable<SalesOrderItem>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Urun).Include(x => x.SalesOrder).AsNoTracking().ToListAsync();
        }
        async Task<string> ISalesOrderItemRepository.Delete(SalesOrderItem entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
