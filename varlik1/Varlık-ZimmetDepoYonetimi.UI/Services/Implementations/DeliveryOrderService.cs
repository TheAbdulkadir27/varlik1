using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private readonly IDeliveryOrderRepository _repository;
        public DeliveryOrderService(IDeliveryOrderRepository repository) => _repository = repository;
        public async Task AddAsync(DeliveryOrder entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var deliveryorder1 = await _repository.GetByIdAsync(id);
            if (deliveryorder1 != null)
            {
                var message = await _repository.Delete(deliveryorder1);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<DeliveryOrder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DeliveryOrder> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var deliveryorder = await GetAllAsync();
            return deliveryorder.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(DeliveryOrder entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
