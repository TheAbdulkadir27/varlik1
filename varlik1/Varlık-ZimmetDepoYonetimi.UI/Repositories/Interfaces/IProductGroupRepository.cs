using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IProductGroupRepository : IGenericRepository<ProductGroup>
    {
        Task<string> Delete(ProductGroup entity);
    }
}
