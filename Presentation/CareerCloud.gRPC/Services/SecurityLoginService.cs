using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class SecurityLoginService : SecurityLogin.SecurityLoginBase
{
    readonly SecurityLoginLogic _logic;

    public SecurityLoginService(ILogger<SecurityLoginService> logger, IDataRepository<SecurityLoginPoco> repository)
    {
        _logic = new SecurityLoginLogic(repository);

    }
}