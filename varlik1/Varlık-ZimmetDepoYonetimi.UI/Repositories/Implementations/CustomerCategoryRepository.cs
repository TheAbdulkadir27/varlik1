using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class CustomerCategoryRepository : GenericRepository<CustomerCategory>, ICustomerCategoryRepository
    {
        public CustomerCategoryRepository(VarlikZimmetEnginContext context) : base(context)
        {

        }
        async Task<string?> ICustomerCategoryRepository.Delete(CustomerCategory entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
    }
}
