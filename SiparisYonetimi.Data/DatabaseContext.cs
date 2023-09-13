using Microsoft.EntityFrameworkCore;
using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Data
{
    public class DatabaseContext:DbContext
    {

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<News> News { get; set; }

        // bu metot veritabanı bağlantı ayarlarını yapabildiğimiz bir metot
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=EMIRHP\SQLEXPRESS;Initial Catalog=SiparisYonetimiDB;User Id=sa;Password=123;TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aşağıdaki HasData metodu veritabanı oluştuktan sonra admin kullanıcısı oluşturmamızı sağlıyor(seed)
            modelBuilder.Entity<User>().HasData(new User
            {
                CreateDate = DateTime.Now,
                Email = "admin@siparisyonetimi.com",
                Id = 1,
                IsActive = true,
                IsAdmin = true,
                Name = "Admin",
                Surname = "Admin",
                Username = "admin",
                Password = "123",
                Phone = "123456789",
                UserGuid=Guid.NewGuid()
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
