using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations
{
    public class CustomerGroupRepository : GenericRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(VarlikZimmetEnginContext context) : base(context)
        {

        }

        async Task<string> ICustomerGroupRepository.Delete(CustomerGroup entity)
        {
            //var Model = await _context..Include(x => x.Uruns).FirstOrDefaultAsync(x => x.ModelId == entity.ModelId);
            //if (Model?.Uruns != null && Model.Uruns.Any())
            //{
            //    return "Bu Model Silinemez Çünkü Bağlı Ürünleri Mevcut";
            //}
            //else
            //{
            //    _dbSet.Remove(Model!);
            //    return "Bu Model Başarıyla Silinmiştir";
            //}
            _dbSet.Remove(entity!);
            return "Başarıyla Silindi";
        }
    }
}
