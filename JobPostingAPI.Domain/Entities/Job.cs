using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Job : BaseEntity
    {
        [Required(ErrorMessage = "Şirket ID'si gereklidir.")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Pozisyon gereklidir.")]
        [StringLength(100, ErrorMessage = "Pozisyon 100 karakterden uzun olamaz.")]
        public string Position { get; set; }

        [StringLength(1000, ErrorMessage = "Açıklama 1000 karakterden uzun olamaz.")]
        public string Description { get; set; }

        public int QualityScore { get; set; }

        public string? Benefits { get; set; }
        public string? EmploymentType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maaş sıfırdan büyük bir değer olmalıdır.")]
        public decimal? Salary { get; set; }

        [Required(ErrorMessage = "Şirket bilgisi gereklidir.")]
        public Company Company { get; set; }

    }

}
