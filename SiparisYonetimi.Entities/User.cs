﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiparisYonetimi.Entities
{
    public class User:IEntity
    {
        public int Id { get; set; }

        [StringLength(50), Required(ErrorMessage = "{0} gereklidir!"), Display(Name = "Adı")] 
        public string Name { get; set; }
        
        [StringLength(50), Required(ErrorMessage = "{0} gereklidir!"), Display(Name = "Soyadı")]
        public string Surname { get; set; }
        
        [StringLength(50), Required(ErrorMessage = "{0} gereklidir!")]
        public string Email { get; set; }
        
        [StringLength(15), Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [StringLength(50), Display(Name = "Kullanıcı Adı")]
        public string? Username { get; set; }
        
        [StringLength(50), Display(Name = "Şifre"), Required(ErrorMessage = "{0} gereklidir!")]
        public string Password { get; set; }
       
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public Guid? UserGuid { get; set; } //bu guid değerini session veya cookie de saklayarak kullanıcıyı tanımak için kullandık
    }
}
