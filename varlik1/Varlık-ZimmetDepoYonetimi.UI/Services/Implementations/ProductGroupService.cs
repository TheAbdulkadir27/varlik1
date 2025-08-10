using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductGroupRepository _repository;
        public ProductGroupService(IProductGroupRepository repository) => _repository = repository;

        public async Task AddAsync(ProductGroup entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var ProductGroup = await _repository.GetByIdAsync(id);
            if (ProductGroup != null)
            {
                var message = await _repository.Delete(ProductGroup);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<ProductGroup>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductGroup> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var ProductGroup = await GetAllAsync();
            return ProductGroup.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateAsync(ProductGroup entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
