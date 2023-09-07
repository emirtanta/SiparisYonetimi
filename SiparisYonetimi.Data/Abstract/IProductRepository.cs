using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        /// <summary>
        /// kategori ve markasına göre tek bir ürün getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductByCategoryAndBrandAsync(int id);

        /// <summary>
        /// ürünleri kategori ve markalarına göre liste şeklinde getirir
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetProductsByCategoryAndBrandAsync();
    }
}
