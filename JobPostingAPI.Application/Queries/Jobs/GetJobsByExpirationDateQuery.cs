using JobPortal.Domain.Entities.Elastic;
using MediatR;
using Nest;

namespace JobPortal.Application.Queries.Jobs;

public class GetJobsByExpirationDateQuery : MediatR.IRequest<IEnumerable<JobElastic>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public GetJobsByExpirationDateQuery(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}
public class GetJobsByExpirationDateQueryHandler : IRequestHandler<GetJobsByExpirationDateQuery, IEnumerable<JobElastic>>
{
    private readonly IElasticClient _elasticClient;

    public GetJobsByExpirationDateQueryHandler(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<JobElastic>> Handle(GetJobsByExpirationDateQuery request, CancellationToken cancellationToken)
    {

        var searchResponse = await _elasticClient.SearchAsync<JobElastic>(x => x
                .Index("jobs")
                .Query(q => q
                    .DateRange(dr => dr
                        .Field(f => f.ExpirationDate)
                        .GreaterThanOrEquals(request.StartDate)
                        .LessThanOrEquals(request.EndDate)
                     )
                 )
             );
  
        if (!searchResponse.IsValid || !searchResponse.Documents.Any())
        {

            return Enumerable.Empty<JobElastic>();
        }

        return searchResponse.Documents;
    }
}
