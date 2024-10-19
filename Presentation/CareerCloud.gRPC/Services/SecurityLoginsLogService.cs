using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class SecurityLoginsLogService : SecurityLoginsLog.SecurityLoginsLogBase
{
    readonly SecurityLoginsLogLogic _logic;

    public SecurityLoginsLogService(ILogger<SecurityLoginsLogService> logger, IDataRepository<SecurityLoginsLogPoco> repository)
    {
        _logic = new SecurityLoginsLogLogic(repository);
    }
}