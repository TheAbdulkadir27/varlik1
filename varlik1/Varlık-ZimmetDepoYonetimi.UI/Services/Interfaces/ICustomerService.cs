using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer Customer);
        Task<string> DeleteCustomerAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCustomerSelectListAsync();
    }
}
