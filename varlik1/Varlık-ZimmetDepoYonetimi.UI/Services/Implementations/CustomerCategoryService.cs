using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class CustomerCategoryService : ICustomerCategoryService
    {
        private readonly ICustomerCategoryRepository _customercategoryrepository;
        public CustomerCategoryService(ICustomerCategoryRepository customercategoryrepository)
        {
            _customercategoryrepository = customercategoryrepository;
        }
        public async Task AddCustomerCategoryAsync(CustomerCategory customergroup)
        {
            await _customercategoryrepository.AddAsync(customergroup);
            await _customercategoryrepository.SaveAsync();
        }

        public async Task<string> DeleteCustomerCategoryAsync(int id)
        {
            var customercategory = await _customercategoryrepository.GetByIdAsync(id);
            if (customercategory != null)
            {
                var message = await _customercategoryrepository.Delete(customercategory);
                await _customercategoryrepository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<CustomerCategory>> GetAllCustomerCategoryAsync()
        {
            return await _customercategoryrepository.GetAllAsync();
        }

        public async Task<CustomerCategory> GetCustomerCategoryByIdAsync(int id)
        {
            return await _customercategoryrepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetCustomerCategorySelectListAsync()
        {
            var customerCategories = await GetAllCustomerCategoryAsync();
            return customerCategories.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateCustomerCategoryAsync(CustomerCategory Customergroup)
        {
            _customercategoryrepository.Update(Customergroup);
            await _customercategoryrepository.SaveAsync();
        }
    }
}
