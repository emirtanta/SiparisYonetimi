using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Category:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori Adı Zorunludur")]
        [Display(Name = "Kategori Adı")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Kategori Açıklaması")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name ="Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name ="Eklenme Tarihi")/*,ScaffoldColumn(false)*/]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public virtual List<Product>? Products { get; set; }
    }
}
