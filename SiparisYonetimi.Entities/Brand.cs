using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Brand:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Marka Adı Zorunludur")]
        [Display(Name ="Marka Adı")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        [StringLength(1000)]
        public string? Logo { get; set; }

        [Display(Name = "Marka Açıklaması")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Eklenme Tarihi")/*,ScaffoldColumn(false)*/]
        public DateTime? CreateDate { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}
