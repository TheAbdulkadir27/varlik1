using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class VendorGroupService : IVendorGroupService
    {
        private readonly IVendorGroupRepository _repository;
        public VendorGroupService(IVendorGroupRepository repository) => _repository = repository;

        public async Task AddAsync(VendorGroup entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var vendorgroup = await _repository.GetByIdAsync(id);
            if (vendorgroup != null)
            {
                var message = await _repository.Delete(vendorgroup);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<VendorGroup>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<VendorGroup> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var vendorgroup = await GetAllAsync();
            return vendorgroup.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(VendorGroup entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
