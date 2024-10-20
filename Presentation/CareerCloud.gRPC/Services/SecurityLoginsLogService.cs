using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Mappers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;

internal class SecurityLoginsLogService : SecurityLoginsLog.SecurityLoginsLogBase
{
    readonly SecurityLoginsLogLogic _logic;

    public SecurityLoginsLogService(ILogger<SecurityLoginsLogService> logger, IDataRepository<SecurityLoginsLogPoco> repository)
    {
        _logic = new SecurityLoginsLogLogic(repository);
    }

    public override Task<Empty> Add(SecurityLoginsLogProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(SecurityLoginsLogProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(SecurityLoginsLogProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<SecurityLoginsLogProto?> Get(GetSecurityLoginsLogRequest request, ServerCallContext context)
    {
        Guid.TryParse(request.Id, out Guid id);
        if (id == Guid.Empty)
            return null;

        var poco = _logic.Get(id);
        return Task.FromResult(poco?.ToProto());
    }
}