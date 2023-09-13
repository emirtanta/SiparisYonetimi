using Microsoft.EntityFrameworkCore;
using SiparisYonetimi.Data.Abstract;
using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context):base(context)
        {
            
        }

        /// <summary>
        /// kategori ve markasına göre tek bir ürün getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<Product> GetProductByCategoryAndBrandAsync(int id)
        {
            return _context.Products.Include(c=>c.Category).Include(b=>b.Brand).FirstOrDefaultAsync(p=>p.Id==id);
        }

        /// <summary>
        /// ürünleri kategori ve markalarına göre liste şeklinde getirir
        /// </summary>
        /// <returns></returns>
        public Task<List<Product>> GetProductsByCategoryAndBrandAsync()
        {
            return _context.Products.Include(c=>c.Category).Include(b=>b.Brand).ToListAsync();
        }

        /// <summary>
        /// tüm ürünleri marka ve kategorisiyle lambda expression filtre uygulayarak getirir
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsByIncludeAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products.Where(expression).Include(p => p.Brand).Include(p => p.Category).ToListAsync();
        }
    }
}
