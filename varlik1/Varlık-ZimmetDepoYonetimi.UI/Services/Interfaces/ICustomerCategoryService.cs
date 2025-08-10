using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ICustomerCategoryService
    {
        Task<IEnumerable<CustomerCategory>> GetAllCustomerCategoryAsync();
        Task<CustomerCategory> GetCustomerCategoryByIdAsync(int id);
        Task AddCustomerCategoryAsync(CustomerCategory customercategory);
        Task UpdateCustomerCategoryAsync(CustomerCategory customercategory);
        Task<string> DeleteCustomerCategoryAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCustomerCategorySelectListAsync();
    }
}
