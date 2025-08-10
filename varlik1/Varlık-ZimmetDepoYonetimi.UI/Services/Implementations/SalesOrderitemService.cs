using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class SalesOrderitemService : ISalesOrderItemService
    {
        private readonly ISalesOrderItemRepository _repository;
        private readonly VarlikZimmetEnginContext _context;
        public SalesOrderitemService(ISalesOrderItemRepository repository, VarlikZimmetEnginContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task AddAsync(SalesOrderItem entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var salesorderıtem1 = await _repository.GetByIdAsync(id);
            if (salesorderıtem1 != null)
            {
                var message = await _repository.Delete(salesorderıtem1);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<SalesOrderItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SalesOrderItem> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var salesorderıtem = await GetAllAsync();
            return salesorderıtem.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.SalesOrder?.Number
            });
        }

        public async Task UpdateAsync(SalesOrderItem entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
        public async Task DeleteAllByOrderIdAsync(int orderId)
        {
            var items = await _context.SalesOrderItem.Where(x => x.SalesOrderId == orderId).ToListAsync();
            _context.SalesOrderItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
