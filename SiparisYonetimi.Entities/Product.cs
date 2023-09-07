using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Product:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün Adı Zorunludur")]
        [Display(Name = "Ürün Adı")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Ürün Fiyatı")]
        public decimal Price { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int Stock { get; set; }

        [Display(Name = "Ürün Resmi")]
        [StringLength(1000)]
        public string? Image { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Anasayfada mı?")]
        public bool IsHome { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }=DateTime.Now;

        [Display(Name = "Kategorisi")]
        public int CategoryId { get; set; }
        public  virtual Category? Category { get; set; }

        [Display(Name = "Markası")]
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
    }
}
