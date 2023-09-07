using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SiparisYonetimi.WebUI.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage ="Email Zorunludur")]
        [StringLength(100)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email Zorunludur")]
        [StringLength(100)]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
