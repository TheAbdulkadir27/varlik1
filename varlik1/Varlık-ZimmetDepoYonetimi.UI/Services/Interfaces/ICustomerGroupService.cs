using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ICustomerGroupService
    {
        Task<IEnumerable<CustomerGroup>> GetAllCustomerGroupAsync();
        Task<CustomerGroup> GetCustomerGroupByIdAsync(int id);
        Task AddCustomerGroupAsync(CustomerGroup customergroup);
        Task UpdateCustomerGroupAsync(CustomerGroup Customergroup);
        Task<string> DeleteCustomerGroupAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCustomerGroupSelectListAsync();
    }
}
