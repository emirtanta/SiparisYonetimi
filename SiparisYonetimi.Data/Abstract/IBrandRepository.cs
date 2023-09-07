using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data.Abstract
{
    public interface IBrandRepository:IRepository<Brand>
    {
        /// <summary>
        /// markayı ürünleri ile birlikte getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Brand> GetBrandByProducts(int id);
    }
}
