using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class VendorRepository : GenericRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }
        async Task<string?> IVendorRepository.Delete(Vendor entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
        public override async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _context.Vendor.Include(x => x.VendorCategory).Include(x => x.VendorGroup).OrderBy(x => x.Name).ToListAsync();
        }
        public async override Task<Vendor> GetByIdAsync(int id)
        {
            return await _context.Vendor.Include(x => x.VendorCategory).Include(x => x.VendorGroup).SingleOrDefaultAsync(x => x.ID == id);
        }
    }

}
