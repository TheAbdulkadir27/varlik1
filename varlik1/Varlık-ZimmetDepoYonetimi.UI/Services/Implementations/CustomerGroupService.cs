using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class CustomerGroupService : ICustomerGroupService
    {
        private readonly ICustomerGroupRepository _CustomerGroupRepository;
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository)
        {
            _CustomerGroupRepository = customerGroupRepository;
        }
        public async Task AddCustomerGroupAsync(CustomerGroup customergroup)
        {
            await _CustomerGroupRepository.AddAsync(customergroup);
            await _CustomerGroupRepository.SaveAsync();
        }

        public async Task<string> DeleteCustomerGroupAsync(int id)
        {
            var customergroup = await _CustomerGroupRepository.GetByIdAsync(id);
            if (customergroup != null)
            {
                var message = await _CustomerGroupRepository.Delete(customergroup);
                await _CustomerGroupRepository.SaveAsync();
                return message;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<CustomerGroup>> GetAllCustomerGroupAsync()
        {
            return await _CustomerGroupRepository.GetAllAsync();
        }

        public async Task<CustomerGroup> GetCustomerGroupByIdAsync(int id)
        {
            return await _CustomerGroupRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetCustomerGroupSelectListAsync()
        {
            var customergroup = await GetAllCustomerGroupAsync();
            return customergroup.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.Name
            });
        }

        public async Task UpdateCustomerGroupAsync(CustomerGroup Customergroup)
        {
            _CustomerGroupRepository.Update(Customergroup);
            await _CustomerGroupRepository.SaveAsync();
        }
    }
}
