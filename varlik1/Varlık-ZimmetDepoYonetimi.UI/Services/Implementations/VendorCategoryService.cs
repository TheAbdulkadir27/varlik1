using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class VendorCategoryService : IVendorCategoryService
    {
        private readonly IVendorCategoryRepository _repository;
        public VendorCategoryService(IVendorCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(VendorCategory entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var vendorcategory = await _repository.GetByIdAsync(id);
            if (vendorcategory != null)
            {
                var message = await _repository.Delete(vendorcategory);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<VendorCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<VendorCategory> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var vendorcategory = await GetAllAsync();
            return vendorcategory.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Description
            });
        }

        public async Task UpdateAsync(VendorCategory entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
