using Microsoft.EntityFrameworkCore;
using P013EStore.Core.Entities;
using P013EStore.Data.Configurations;
using System.Reflection;

namespace P013EStore.Data
{
    public class DatabaseContext :DbContext
    {
        //Katmanlı mimaride bir proje katmanından başka bir katmana erişebilmek için bulunduğumuz data projesinin dependencies kısmına sağ tıklayıp add project references diyerek açılan pencereden Core Projesine tik atıp ok diyerek pencereyi kapatmamız gerekiyor.
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Contact>  Contacts { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Slider>  Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // onconfiguring metodu veritabanı ile bağlantı ayarlarını yapmamızı sağlar
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=P013EStore; Trusted_Connection=True");
            //optionsBuilder.UseSqlServer=CanlıServerAdı; Database=CanlıdakiDatabase; Username=CanlıVeritabanıKullanıcıAdı; Password=CanlıVeritabanıŞifre");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluentAPI ile veritabanı tablolarımız oluşurken veri tiplerini db kurallarını burada tanımlayabiliriz.
            modelBuilder.Entity<AppUser>().Property(a=> a.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50); // FluentAPI İle AppUser clasının Name Property si için oluşacak veritabanı kolonu ayarını bu şekilde belirleyebiliyoruz
            modelBuilder.Entity<AppUser>().Property(a => a.Surname).HasColumnType("varchar(50)").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.UserName).HasColumnType("varchar(50)").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Password).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(a => a.Email).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Phone).HasMaxLength(20);
            //FluentAPI HasData ile Db oluştuktan sonra başlangıç kayıtları ekleme 
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = 1,
                Email="info@P013EStore.com",
                Password ="123",
                UserName="Admin",
                IsActive=true,
                IsAdmin=true,
                Name="Admin",
                UserGuid=new Guid(), // kullanıcıya benzersiz bir id no oluştur
            });
            // modelBuilder.ApplyConfiguration(new BrandConfigurations()); // marka için yaptığımız konfigürasyon ayarlarını çağırdık.
            // modelBuilder.ApplyConfiguration(new CategoryConfigurations()); //Category için yaptığımız konfigürasyon ayarlarını çağırdık.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // uygulamadaki tüm configurations classlarını burada çalıştır
           
            // fluent validation data anotationdaki hata mesajları vb işlemleri yönetebileceğimiz 3. parti paket.
            
            // Katmanlı mimaride MvcWebUI katmanından direk data katmanına erişilmesi istenmez, arada bir iş katmanının tüm db süreçlerini yönetmesi istenir.Bu yüzden solutiona service katmanı ekleyip Mvc katmanından service katmanına erişim vermemiz gerekir.Service katmanı da data katmanına erişir.Data katmanı da core katmanına erişir, böylece MVCUI > Service > Data > Core ile en üstten en alt katmana kadar ulaşılabilmiş olunur.
            base.OnModelCreating(modelBuilder);
        }



    }
}