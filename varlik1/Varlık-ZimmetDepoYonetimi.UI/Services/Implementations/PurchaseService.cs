using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;
        public PurchaseService(IPurchaseRepository repository) => _repository = repository;
        public async Task AddAsync(PurchaseOrder entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var purchaseOrder = await _repository.GetByIdAsync(id);
            if (purchaseOrder != null)
            {
                var message = await _repository.Delete(purchaseOrder);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var purchases = await GetAllAsync();
            return purchases.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(PurchaseOrder entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
