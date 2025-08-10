using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class UnitMeasureRepository : GenericRepository<UnitMeasure>, IUnitMeasureRepository
    {
        public UnitMeasureRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        async Task<string> IUnitMeasureRepository.Delete(UnitMeasure entity)
        {
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
    }
}
