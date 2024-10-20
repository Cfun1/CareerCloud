using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Mappers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;

internal class ApplicantProfileService : ApplicantProfile.ApplicantProfileBase
{
    readonly ApplicantProfileLogic _logic;

    public ApplicantProfileService(ILogger<ApplicantProfileService> logger, IDataRepository<ApplicantProfilePoco> repository)
    {
        _logic = new ApplicantProfileLogic(repository);
        // _logger = logger;
    }

    public override Task<Empty> Add(ApplicantProfileProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(ApplicantProfileProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(ApplicantProfileProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<ApplicantProfileProto?> Get(GetApplicantProfileRequest request, ServerCallContext context)
    {
        Guid.TryParse(request.Id, out Guid id);
        if (id == Guid.Empty)
            return null;

        var poco = _logic.Get(id);
        return Task.FromResult(poco?.ToProto());
    }
}