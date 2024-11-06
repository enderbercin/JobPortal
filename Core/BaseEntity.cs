using Core.Enums;


namespace Core;

public abstract class BaseEntity
{


    public Guid Id { get; set; } = Guid.NewGuid();

    public RecordStatus RecordStatus { get; set; }


    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ChangedDate { get; set; }

}
