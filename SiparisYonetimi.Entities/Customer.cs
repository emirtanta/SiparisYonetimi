using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Customer:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Müşteri Adı Zorunludur")]
        [Display(Name = "Müşteri Adı")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Müşteri Soyadı Zorunludur")]
        [Display(Name = "Müşteri Soyadı")]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Müşteri Email Zorunludur")]
        [Display(Name = "Email")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Müşteri Telefon")]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Display(Name = "Müşteri Adres")]
        [StringLength(1000)]
        public string? Address { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
