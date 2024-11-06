using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Company : BaseEntity
    {
        [Required(ErrorMessage = "Şirket adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Şirket adı 100 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adres bilgisi gereklidir.")]
        [StringLength(500, ErrorMessage = "Adres 500 karakterden uzun olamaz.")]
        public string Address { get; set; }
        public int PostingQuota { get; set; } = 2; 

        public ICollection<Job> Jobs { get; set; } = new List<Job>();

    }
}
