using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Mappers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;

internal class CompanyJobEducationService : CompanyJobEducation.CompanyJobEducationBase
{
    readonly CompanyJobEducationLogic _logic;

    public CompanyJobEducationService(ILogger<CompanyJobEducationService> logger, IDataRepository<CompanyJobEducationPoco> repository)
    {
        _logic = new CompanyJobEducationLogic(repository);
    }

    public override Task<Empty> Add(CompanyJobEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(CompanyJobEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(CompanyJobEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<CompanyJobEducationProto?> Get(GetCompanyJobEducationRequest request, ServerCallContext context)
    {
        Guid.TryParse(request.Id, out Guid id);
        if (id == Guid.Empty)
            return null;

        var poco = _logic.Get(id);
        return Task.FromResult(poco?.ToProto());
    }
}