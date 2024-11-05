using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int PostingQuota { get; set; } = 2; // Initial quota of 2 postings

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
