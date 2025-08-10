using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _repository;
        public VendorService(IVendorRepository repository) => _repository = repository;

        public async Task AddAsync(Vendor vendor)
        {
            await _repository.AddAsync(vendor);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var vendor = await _repository.GetByIdAsync(id);
            if (vendor != null)
            {
                var message = await _repository.Delete(vendor);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Vendor> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var vendors = await GetAllAsync();
            return vendors.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(Vendor vendor)
        {
            _repository.Update(vendor);
            await _repository.SaveAsync();
        }
    }
}
