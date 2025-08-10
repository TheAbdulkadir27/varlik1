using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _repository;
        public TaxService(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTaxAsync(Tax tax)
        {
            await _repository.AddAsync(tax);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteTaxAsync(int id)
        {
            var tax = await _repository.GetByIdAsync(id);
            if (tax != null)
            {
                var message = await _repository.Delete(tax);
                await _repository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<Tax>> GetAllTaxAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tax> GetTaxByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetTaxSelectListAsync()
        {
            var taxes = await GetAllTaxAsync();
            return taxes.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateTaxAsync(Tax tax)
        {
            _repository.Update(tax);
            await _repository.SaveAsync();
        }
    }
}
