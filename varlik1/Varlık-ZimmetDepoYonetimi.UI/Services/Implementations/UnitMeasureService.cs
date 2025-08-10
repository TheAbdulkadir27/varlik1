using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class UnitMeasureService : IUnitMeasureService
    {
        private readonly IUnitMeasureRepository _repository;
        public UnitMeasureService(IUnitMeasureRepository repository) => _repository = repository;

        public async Task AddAsync(UnitMeasure entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var unitmeasure = await _repository.GetByIdAsync(id);
            if (unitmeasure != null)
            {
                var message = await _repository.Delete(unitmeasure);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<UnitMeasure>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UnitMeasure> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var unitmeasure = await GetAllAsync();
            return unitmeasure.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateAsync(UnitMeasure entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
