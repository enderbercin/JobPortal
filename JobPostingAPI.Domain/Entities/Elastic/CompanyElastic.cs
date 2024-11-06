using Core.Enums;

namespace JobPortal.Domain.Entities.Elastic;

public class CompanyElastic
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int PostingQuota { get; set; }
    public RecordStatus RecordStatus { get; set; }

}
