using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace P013EStore.Core.Entities
{
    public class Contact:IEntity
    {
        public int Id { get; set; }
       
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string? Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Mesajınız"),DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        
    }

}
