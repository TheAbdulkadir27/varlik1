using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class VendorContactRepository : GenericRepository<VendorContact>, IVendorContactRepository
    {
        public VendorContactRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        async Task<string> IVendorContactRepository.Delete(VendorContact entity)
        {
            _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
        public override async Task<IEnumerable<VendorContact>> GetAllAsync()
        {
            return await _context.VendorContacts.Include(x => x.Vendor).ToListAsync();
        }
        public override async Task<VendorContact> GetByIdAsync(int id)
        {
            return await _context.VendorContacts.Include(x => x.Vendor).FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
