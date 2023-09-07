using Microsoft.EntityFrameworkCore;
using SiparisYonetimi.Data.Abstract;
using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context):base(context)
        {
            
        }

        /// <summary>
        /// kategoriyi ürünleriyle birlikte getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Category> GetCategoryByProducts(int id)
        {
            return await _dbSet.Where(c => c.Id == id).AsNoTracking().Include(p => p.Products).FirstOrDefaultAsync();
        }
    }
}
