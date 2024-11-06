using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public abstract class BaseEntity
    {


        public Guid Id { get; set; } = Guid.NewGuid();

        public RecordStatus RecordStatus { get; set; }


        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ChangedDate { get; set; }

    }
    public enum RecordStatus
    {
        Active = 1,
        Passive
    }
}
