using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IModelRepository : IGenericRepository<Model>
    {
        Task<string> Delete(Model entity);
    }
}