using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class News:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur")]
        [Display(Name = "Başlık")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Başlık alanı en fazla 50 karakter olmalıdır")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(1000)]
        [MaxLength(1000, ErrorMessage = "Açıklama alanı en fazla 1000 karakter olmalıdır")]
        public string? Description { get; set; }

        [Display(Name = "Resim")]
        [StringLength(1000)]
        public string? Image { get; set; }

        [Display(Name ="Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Anasayfada Göster")]
        public bool IsHome { get; set; }

        [Display(Name ="Sıra No")]
        public int? OrderNo { get; set; }

        [Display(Name ="Eklenme Tarihi")]
        public DateTime? CreateDate { get; set; }
    }
}
