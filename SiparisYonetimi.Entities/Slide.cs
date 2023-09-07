using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Slide:IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Resim")]
        [StringLength(1000)]
        public string? Image { get; set; }

        [Display(Name = "Başlık")]
        [StringLength(100)]
        public string? Title { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(1000)]
        public string? Description { get; set; }


        [Display(Name = "Link")]
        [StringLength(1000)]
        public string? Link { get; set; }
    }
}
