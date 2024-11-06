using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities.Elastic
{
    public class JobElastic
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int QualityScore { get; set; }
    }

}
