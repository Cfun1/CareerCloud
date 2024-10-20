using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Mappers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;

internal class SystemLanguageCodeService : SystemLanguageCode.SystemLanguageCodeBase
{
    readonly SystemLanguageCodeLogic _logic;

    public SystemLanguageCodeService(ILogger<SystemLanguageCodeService> logger, IDataRepository<SystemLanguageCodePoco> repository)
    {
        _logic = new SystemLanguageCodeLogic(repository);
    }

    public override Task<Empty> Add(SystemLanguageCodeProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(SystemLanguageCodeProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(SystemLanguageCodeProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<SystemLanguageCodeProto?> Get(GetSystemLanguageCodeRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.LanguageID))
            return null;

        var poco = _logic.Get(request.LanguageID);
        return Task.FromResult(poco?.ToProto());
    }
}