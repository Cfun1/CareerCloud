using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Mappers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;

internal class CompanyJobService : CompanyJob.CompanyJobBase
{
    readonly CompanyJobLogic _logic;

    public CompanyJobService(ILogger<CompanyJobService> logger, IDataRepository<CompanyJobPoco> repository)
    {
        _logic = new CompanyJobLogic(repository);
    }

    public override Task<Empty> Add(CompanyJobProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(CompanyJobProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(CompanyJobProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<CompanyJobProto?> Get(GetCompanyJobRequest request, ServerCallContext context)
    {
        Guid.TryParse(request.Id, out Guid id);
        if (id == Guid.Empty)
            return null;

        var poco = _logic.Get(id);
        return Task.FromResult(poco?.ToProto());
    }
}