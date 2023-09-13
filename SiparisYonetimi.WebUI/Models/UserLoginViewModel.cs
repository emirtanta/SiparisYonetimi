

using System.ComponentModel.DataAnnotations;

namespace SiparisYonetimi.WebUI.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="Email zorunludur")]
        [Display(Name ="Email")]
        [MaxLength(50,ErrorMessage ="Email alanı 50 karakterden fazla olamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [Display(Name = "Şifre")]
        [MaxLength(50, ErrorMessage = "Şifre alanı 50 karakterden fazla olamaz")]
        [MinLength(3, ErrorMessage = "Şifre alanı 3 karakterden az olamaz")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string? ReturnUrl { get; set; }
    }
}
