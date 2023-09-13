using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiparisYonetimi.Entities
{
    public class Setting:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Site başlık alanı zorunludur")]
        [Display(Name = "Site Başlık")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Site başlık alanı en fazla 50 karakter olmalıdır")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(1000)]
        [MaxLength(1000, ErrorMessage = "Açıklama alanı en fazla 1000 karakter olmalıdır")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Email alanı en fazla 50 karakter olmalıdır")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Telefon")]
        [StringLength(15)]
        [MaxLength(15, ErrorMessage = "Telefon alanı en fazla 15 karakter olmalıdır")]
        public string? Phone { get; set; }

        [Display(Name = "Mail Sunucusu")]
        [StringLength(500)]
        [MaxLength(500, ErrorMessage = "Mail sunucusu alanı en fazla 500 karakter olmalıdır")]
        public string? MailServer { get; set; }

        [Display(Name = "Port")]
        public int? Port { get; set; }

        [Required(ErrorMessage = "Mail kullanıcı adı alanı zorunludur")]
        [Display(Name = "Mail Kullanıcı Adı")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Mail kullanıcı adı alanı en fazla 50 karakter olmalıdır")]
        public string? UserName { get; set; }

        [Display(Name = "Mail Şifresi")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Maiş şifresi alanı en fazla 50 karakter olmalıdır")]
        public string? Password { get; set; }

        [Display(Name = "İkon")]
        [StringLength(1000)]
        public string? Favicon { get; set; }

        [Display(Name = "Site Logo")]
        [StringLength(1000)]
        public string? Logo { get; set; }

        [Display(Name = "Firma Adresi")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Firma adresi alanı en fazla 50 karakter olmalıdır")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        [Display(Name = "Firma Harita Kodu")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string? MapCode { get; set; }
    }
}
