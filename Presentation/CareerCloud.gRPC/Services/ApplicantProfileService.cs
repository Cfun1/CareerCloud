using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class ApplicantProfileService
{
    readonly ApplicantProfileLogic _logic;

    public ApplicantProfileService(ILogger<ApplicantProfileService> logger, IDataRepository<ApplicantProfilePoco> repository)
    {
        _logic = new ApplicantProfileLogic(repository);
        // _logger = logger;
    }
}