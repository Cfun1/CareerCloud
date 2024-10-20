using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class CompanyJobEducationMapper
{
    public static CompanyJobEducationPoco[]? ToPoco(this CompanyJobEducationProtos protos)
    {
        var pocos = new List<CompanyJobEducationPoco>();
        foreach (CompanyJobEducationProto proto in protos.Proto)
        {
            pocos.Add(new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(proto.Id),
                Job = Guid.Parse(proto.Job),
                Importance = (short)proto.Importance,
                Major = proto.Major,
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static CompanyJobEducationProtos ToProto(this CompanyJobEducationPoco[] pocos)
    {
        var protos = new CompanyJobEducationProtos();
        foreach (CompanyJobEducationPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static CompanyJobEducationProto ToProto(this CompanyJobEducationPoco poco)
        => new CompanyJobEducationProto()
        {
            Id = poco.Id.ToString(),
            Job = poco.Job.ToString(),
            Major = poco.Major,
            Importance = (int)poco.Importance,
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
