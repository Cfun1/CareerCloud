using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Services;

internal class SystemLanguageCodeService : SystemLanguageCode.SystemLanguageCodeBase
{
    readonly SystemLanguageCodeLogic _logic;

    public SystemLanguageCodeService(ILogger<SystemLanguageCodeService> logger, IDataRepository<SystemLanguageCodePoco> repository)
    {
        _logic = new SystemLanguageCodeLogic(repository);
    }
}