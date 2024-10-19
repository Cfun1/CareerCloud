using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class CompanyJobService : CompanyJob.CompanyJobBase
{
    readonly CompanyJobLogic _logic;

    public CompanyJobService(ILogger<CompanyJobService> logger, IDataRepository<CompanyJobPoco> repository)
    {
        _logic = new CompanyJobLogic(repository);

    }
}