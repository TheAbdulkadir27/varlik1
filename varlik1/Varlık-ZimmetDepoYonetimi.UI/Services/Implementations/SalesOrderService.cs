using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _repository;
        public SalesOrderService(ISalesOrderRepository repository) => _repository = repository;

        public async Task AddAsync(SalesOrder entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var salesorder1 = await _repository.GetByIdAsync(id);
            if (salesorder1 != null)
            {
                var message = await _repository.Delete(salesorder1);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SalesOrder> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var salesorder = await GetAllAsync();
            return salesorder.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(SalesOrder entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
