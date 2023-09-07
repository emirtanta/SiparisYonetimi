using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Contact:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim Zorunludur")]
        [Display(Name = "İsim")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim Zorunludur")]
        [Display(Name = "Soyisim")]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email Zorunludur")]
        [Display(Name = "Email")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj Zorunludur")]
        [Display(Name = "Mesaj")]
        [StringLength(1000)]
        public string Message { get; set; }

        [Display(Name ="Eklenme Tarihi"),ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
