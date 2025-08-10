using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task AddModelAsync(Model model)
        {
            await _modelRepository.AddAsync(model);
            await _modelRepository.SaveAsync();
        }

        public async Task UpdateModelAsync(Model model)
        {
            _modelRepository.Update(model);
            await _modelRepository.SaveAsync();
        }

        public async Task<string> DeleteModelAsync(int id)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model != null)
            {
                var message = await _modelRepository.Delete(model);
                await _modelRepository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<SelectListItem>> GetModelSelectListAsync()
        {
            var models = await GetAllModelsAsync();
            return models.Select(m => new SelectListItem
            {
                Value = m.ModelId.ToString(),
                Text = m.ModelAdi
            });
        }
    }
}