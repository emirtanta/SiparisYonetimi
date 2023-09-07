using SiparisYonetimi.Data.Abstract;
using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Service.Abstract
{
    public interface IService<T>:IRepository<T>where T : class,IEntity,new()
    {
    }
}
