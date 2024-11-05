using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Job : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(15);
        public int QualityScore { get; set; }

        public string? Benefits { get; set; } // Optional
        public string? EmploymentType { get; set; } // Optional
        public decimal? Salary { get; set; } // Optional
        public Company Company { get; set; }
    }
}
