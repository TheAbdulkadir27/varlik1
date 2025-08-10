using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _repository.AddAsync(customer);
            await _repository.SaveAsync();
        }

        public async Task<string> DeleteCustomerAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer != null)
            {
                var message = await _repository.Delete(customer);
                await _repository.SaveAsync();
                return message;
            }
            return "Başarıyla Silindi";
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetCustomerSelectListAsync()
        {
            var customer = await GetAllCustomerAsync();
            return customer.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateCustomerAsync(Customer Customer)
        {
            _repository.Update(Customer);
            await _repository.SaveAsync();
        }
    }
}
