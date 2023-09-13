using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class CartLine
    {
        public Product Product { get; set; }

        [Display(Name ="Adet")]
        public int Quantity { get; set; }
    }
}
