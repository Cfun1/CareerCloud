using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class ApplicantEducationMapper
{
    public static ApplicantEducationPoco[]? ToPoco(this ApplicantEducationProtos protos)
    {
        var pocos = new List<ApplicantEducationPoco>();
        foreach (ApplicantEducationProto proto in protos.ApplicantEducations)
        {
            pocos.Add(new ApplicantEducationPoco()
            {
                Id = Guid.Parse(proto.Id),
                Applicant = Guid.Parse(proto.Applicant),
                CertificateDiploma = proto.CertificateDiploma,
                CompletionDate = proto.CompletionDate.ToDateTime(),
                CompletionPercent = (byte)proto.CompletionPercent!,
                Major = proto.Major,
                StartDate = proto.StartDate.ToDateTime(),
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static ApplicantEducationProtos ToProto(this ApplicantEducationPoco[] pocos)
    {
        var protos = new ApplicantEducationProtos();
        foreach (ApplicantEducationPoco poco in pocos)
        {
            protos.ApplicantEducations.Add(poco.ToProto());
        }
        return protos;
    }

    public static ApplicantEducationProto ToProto(this ApplicantEducationPoco poco)
        => new ApplicantEducationProto()
        {
            Id = poco.Id.ToString(),
            Applicant = poco.Applicant.ToString(),
            Major = poco.Major,
            CompletionDate = poco.CompletionDate.DateTime2ProtoTimeStamp(),
            CertificateDiploma = poco.CertificateDiploma,
            StartDate = poco.StartDate.DateTime2ProtoTimeStamp(),
            CompletionPercent = (uint)poco.CompletionPercent!,
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
//uint protobufValue = nullableByte.HasValue ? nullableByte.Value : 0; // Assuming 0 indicates null
//    Value = nullableByte.HasValue ? new Google.Protobuf.UInt32Value { Value = nullableByte.Value } : null
