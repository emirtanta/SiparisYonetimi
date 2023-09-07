using SiparisYonetimi.Data;
using SiparisYonetimi.Data.Concrete;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Service.Concrete
{
    public class Service<T>:Repository<T>,IService<T>where T : class,IEntity,new()
    {
        public Service(DatabaseContext context):base(context)
        {
            
        }
    }
}
