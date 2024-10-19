using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class CompanyJobEducationService : CompanyJobEducation.CompanyJobEducationBase
{
    readonly CompanyDescriptionLogic _logic;

    public CompanyJobEducationService(ILogger<CompanyJobEducationService> logger, IDataRepository<CompanyJobEducationPoco> repository)
    {
        _logic = new CompanyDescriptionLogic(repository);

    }
}