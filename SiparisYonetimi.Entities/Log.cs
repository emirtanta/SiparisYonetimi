using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Log:IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        [StringLength(50)]
        public string? Title { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name ="Oluşma Tarihi")]
        public DateTime? CreateDate { get; set; }
    }
}
