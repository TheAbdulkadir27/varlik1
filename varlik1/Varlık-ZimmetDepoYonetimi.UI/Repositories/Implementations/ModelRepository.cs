using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class ModelRepository : GenericRepository<Model>, IModelRepository
    {
        public ModelRepository(VarlikZimmetEnginContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.UstModel)
                .ToListAsync();
        }

        public override async Task<Model?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.UstModel)
                .FirstOrDefaultAsync(m => m.ModelId == id);
        }

        public async Task<string> Delete(Model entity)
        {
            var Model = await _context.Model.Include(x => x.Uruns).FirstOrDefaultAsync(x => x.ModelId == entity.ModelId);
            if (Model?.Uruns != null && Model.Uruns.Any())
            {
                return "Bu Model Silinemez Çünkü Bağlı Ürünleri Mevcut";
            }
            else
            {
                _dbSet.Remove(Model!);
                return "Bu Model Başarıyla Silinmiştir";
            }
        }
    }
}