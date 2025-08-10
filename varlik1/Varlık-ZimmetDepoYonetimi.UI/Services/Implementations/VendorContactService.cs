using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class VendorContactService : IVendorContactService
    {
        private readonly IVendorContactRepository _repository;

        public VendorContactService(IVendorContactRepository repository) => _repository = repository;
        public async Task AddAsync(VendorContact vendorcontact)
        {
            await _repository.AddAsync(vendorcontact);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var vendorcontact = await _repository.GetByIdAsync(id);
            if (vendorcontact != null)
            {
                var message = await _repository.Delete(vendorcontact);
                await _repository.SaveAsync();
                return message;
            }
            return "Başarıyla Silindi";
        }

        public async Task<IEnumerable<VendorContact>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<VendorContact> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var vendorcontact = await GetAllAsync();
            return vendorcontact.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateAsync(VendorContact vendorcontact)
        {
            _repository.Update(vendorcontact);
            await _repository.SaveAsync();
        }
    }
}
