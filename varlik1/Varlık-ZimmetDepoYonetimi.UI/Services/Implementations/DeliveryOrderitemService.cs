using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class DeliveryOrderitemService : IDeliveryOrderitemService
    {
        private readonly IDeliveryOrderitemRepository _repository;
        public DeliveryOrderitemService(IDeliveryOrderitemRepository repository) => _repository = repository;

        public async Task AddAsync(DeliveryOrderitem entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAllByOrderIdAsync(int orderId)
        {
           
        }

        public async Task<string> DeleteAsync(int id)
        {
            var deliveryorderitem1 = await _repository.GetByIdAsync(id);
            if (deliveryorderitem1 != null)
            {
                var message = await _repository.Delete(deliveryorderitem1);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<DeliveryOrderitem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DeliveryOrderitem> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var deliveryorderitem = await GetAllAsync();
            return deliveryorderitem.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.DeliveryOrderId!.Value.ToString()
            });
        }

        public async Task UpdateAsync(DeliveryOrderitem entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
