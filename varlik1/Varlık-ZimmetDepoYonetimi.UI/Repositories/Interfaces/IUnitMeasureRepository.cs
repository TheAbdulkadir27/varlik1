using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces
{
    public interface IUnitMeasureRepository : IGenericRepository<UnitMeasure>
    {
        Task<string> Delete(UnitMeasure entity);
    }
}
