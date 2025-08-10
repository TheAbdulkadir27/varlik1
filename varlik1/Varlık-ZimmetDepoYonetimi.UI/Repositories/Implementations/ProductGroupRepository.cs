using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class ProductGroupRepository : GenericRepository<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        async Task<string> IProductGroupRepository.Delete(ProductGroup entity)
        {
           _dbSet.Remove(entity);
            return "Başarıyla Silindi";
        }
    }
}
