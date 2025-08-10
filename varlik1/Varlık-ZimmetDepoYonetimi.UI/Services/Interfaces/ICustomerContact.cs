using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
namespace Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces
{
    public interface ICustomerContactService
    {
        Task<IEnumerable<CustomerContact>> GetAllCustomerContactAsync();
        Task<CustomerContact> GetCustomerContactByIdAsync(int id);
        Task AddCustomerContactAsync(CustomerContact customercontact);
        Task UpdateCustomerContactAsync(CustomerContact Customercontact);
        Task<string> DeleteCustomerContactAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCustomerContactSelectListAsync();
    }
}
