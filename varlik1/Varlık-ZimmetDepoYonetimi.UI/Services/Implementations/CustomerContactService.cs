using Microsoft.AspNetCore.Mvc.Rendering;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class CustomerContactService : ICustomerContactService
    {
        private readonly ICustomerContactRepository _repository;
        public CustomerContactService(ICustomerContactRepository repository)
        {
            _repository = repository;
        }

        public async Task AddCustomerContactAsync(CustomerContact customercontact)
        {
            await _repository.AddAsync(customercontact);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteCustomerContactAsync(int id)
        {
            var customercontact = await _repository.GetByIdAsync(id);
            if (customercontact != null)
            {
                var message = await _repository.Delete(customercontact);
                await _repository.SaveAsync();
                return message;
            }
            return "Başarıyla Silindi";
        }

        public async Task<IEnumerable<CustomerContact>> GetAllCustomerContactAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CustomerContact> GetCustomerContactByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetCustomerContactSelectListAsync()
        {
            var customercontact = await GetAllCustomerContactAsync();
            return customercontact.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateCustomerContactAsync(CustomerContact customercontact)
        {
            _repository.Update(customercontact);
            await _repository.SaveAsync();
        }
    }
}
