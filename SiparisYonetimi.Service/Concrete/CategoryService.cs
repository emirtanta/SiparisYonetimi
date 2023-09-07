using SiparisYonetimi.Data;
using SiparisYonetimi.Data.Concrete;
using SiparisYonetimi.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Service.Concrete
{
    public class CategoryService:CategoryRepository,ICategoryService
    {
        public CategoryService(DatabaseContext context):base(context)
        {
            
        }
    }
}
