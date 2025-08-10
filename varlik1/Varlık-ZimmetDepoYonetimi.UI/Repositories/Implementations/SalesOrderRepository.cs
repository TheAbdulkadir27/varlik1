using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class SalesOrderRepository : GenericRepository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        public override async Task<SalesOrder> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Customer).Include(x => x.Tax).Include(x=>x.SalesOrderItemList).ThenInclude(x=>x.Urun).AsNoTracking().SingleAsync(x => x.Id == id);
        }
        public async override Task<IEnumerable<SalesOrder>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Customer).Include(x => x.Tax).Include(x=>x.SalesOrderItemList).ThenInclude(x=>x.Urun).AsNoTracking().ToListAsync();
        }
        async Task<string> ISalesOrderRepository.Delete(SalesOrder entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
