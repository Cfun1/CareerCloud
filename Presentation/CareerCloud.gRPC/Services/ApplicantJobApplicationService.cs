using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class ApplicantJobApplicationService
{
    readonly ApplicantJobApplicationLogic _logic;

    public ApplicantJobApplicationService(ILogger<ApplicantJobApplicationService> logger, IDataRepository<ApplicantJobApplicationPoco> repository)
    {
        _logic = new ApplicantJobApplicationLogic(repository);
        // _logger = logger;
    }
}