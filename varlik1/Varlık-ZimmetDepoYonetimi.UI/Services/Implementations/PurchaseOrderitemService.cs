using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class PurchaseOrderitemService : IPurchaseOrderitemService
    {
        private readonly IPurchaseOrderitemRepository _repository;
        private readonly VarlikZimmetEnginContext _context;
        public PurchaseOrderitemService(IPurchaseOrderitemRepository repository, VarlikZimmetEnginContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task AddAsync(PurchaseOrderItem entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAllByOrderIdAsync(int purchaseId)
        {
            var items = await _context.PurchaseOrderItems.Where(x => x.PurchaseOrderId == purchaseId).ToListAsync();
            _context.PurchaseOrderItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var purchaseorderitem = await _repository.GetByIdAsync(id);
            if (purchaseorderitem != null)
            {
                var message = await _repository.Delete(purchaseorderitem);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<PurchaseOrderItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PurchaseOrderItem> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var purchaseorderıtem = await GetAllAsync();
            return purchaseorderıtem.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.PurchaseOrder?.Number
            });
        }

        public async Task UpdateAsync(PurchaseOrderItem entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
