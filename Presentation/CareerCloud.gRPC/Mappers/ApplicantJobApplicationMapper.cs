using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class ApplicantJobApplicationMapper
{
    public static ApplicantJobApplicationPoco[]? ToPoco(this ApplicantJobApplicationProtos protos)
    {
        var pocos = new List<ApplicantJobApplicationPoco>();
        foreach (ApplicantJobApplicationProto proto in protos.Proto)
        {
            pocos.Add(new ApplicantJobApplicationPoco()
            {
                Id = Guid.Parse(proto.Id),
                Applicant = Guid.Parse(proto.Applicant),
                ApplicationDate = proto.ApplicationDate.ToDateTime(),
                Job = Guid.Parse(proto.Job),
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static ApplicantJobApplicationProtos ToProto(this ApplicantJobApplicationPoco[] pocos)
    {
        var protos = new ApplicantJobApplicationProtos();
        foreach (ApplicantJobApplicationPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static ApplicantJobApplicationProto ToProto(this ApplicantJobApplicationPoco poco)
        => new ApplicantJobApplicationProto()
        {
            Id = poco.Id.ToString(),
            Applicant = poco.Applicant.ToString(),
            Job = poco.Job.ToString(),
            ApplicationDate = poco.ApplicationDate.DateTime2ProtoTimeStamp(),
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
