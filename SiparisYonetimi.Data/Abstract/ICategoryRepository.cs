using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        /// <summary>
        /// kategoriyi ürünleriyle birlikte getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category> GetCategoryByProducts(int id);
    }
}
