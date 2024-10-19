using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class CompanyDescriptionService : CompanyDescription.CompanyDescriptionBase
{
    readonly CompanyDescriptionLogic _logic;

    public CompanyDescriptionService(ILogger<CompanyDescriptionService> logger, IDataRepository<CompanyDescriptionPoco> repository)
    {
        _logic = new CompanyDescriptionLogic(repository);

    }
}