using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class CustomerContactRepository : GenericRepository<CustomerContact>, ICustomerContactRepository
    {
        public CustomerContactRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        async Task<string?> ICustomerContactRepository.Delete(CustomerContact entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla silindi";
        }
        public override async Task<IEnumerable<CustomerContact>> GetAllAsync()
        {
            return await _context.CustomerContacts.Include(x => x.Customer).ToListAsync();
        }
        public override async Task<CustomerContact> GetByIdAsync(int id)
        {
            return await _context.CustomerContacts.Include(x => x.Customer).FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
