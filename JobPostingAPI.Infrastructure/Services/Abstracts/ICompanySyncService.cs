namespace JobPortal.Application.Services.Abstracts;

public interface ICompanySyncService
{
    Task AddOrUpdateCompanyToElastic(Guid companyId);

}
