using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.gRPC.Helpers;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services;
public class ApplicantEducationService : ApplicantEducation.ApplicantEducationBase
{
    readonly ApplicantEducationLogic _logic;

    public ApplicantEducationService(ILogger<ApplicantEducationService> logger, IDataRepository<ApplicantEducationPoco> repository)
    {
        _logic = new ApplicantEducationLogic(repository);
        // _logger = logger;
    }
    public override Task<Empty> Add(ApplicantEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Add(pocos);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(ApplicantEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Delete(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Update(ApplicantEducationProtos request, ServerCallContext context)
    {
        var pocos = request.ToPoco();
        if (pocos is not null)
            _logic.Update(pocos);

        return Task.FromResult(new Empty());
    }

    public override Task<ApplicantEducationProto?> Get(GetApplicantEducationRequest request, ServerCallContext context)
    {
        Guid.TryParse(request.Id, out Guid id);
        if (id == Guid.Empty)
            return null;

        var poco = _logic.Get(id);
        return Task.FromResult(poco?.ToProto());
    }
}