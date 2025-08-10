using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        async Task<string?> ICustomerRepository.Delete(Customer entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customer.Include(x => x.CustomerCategory).Include(x => x.CustomerGroup).OrderBy(x => x.Name).ToListAsync();
        }
        public async override Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customer.Include(x => x.CustomerCategory).Include(x => x.CustomerGroup).SingleOrDefaultAsync(x => x.ID == id);
        }
    }
}
