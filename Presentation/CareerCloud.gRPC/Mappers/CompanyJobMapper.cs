using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class CompanyJobMapper
{
    public static CompanyJobPoco[]? ToPoco(this CompanyJobProtos protos)
    {
        var pocos = new List<CompanyJobPoco>();
        foreach (CompanyJobProto proto in protos.Proto)
        {
            pocos.Add(new CompanyJobPoco()
            {
                Id = Guid.Parse(proto.Id),
                Company = Guid.Parse(proto.Company),
                IsCompanyHidden = proto.IsCompanyHidden,
                IsInactive = proto.IsInactive,
                ProfileCreated = proto.ProfileCreated.ToDateTime(),
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static CompanyJobProtos ToProto(this CompanyJobPoco[] pocos)
    {
        var protos = new CompanyJobProtos();
        foreach (CompanyJobPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static CompanyJobProto ToProto(this CompanyJobPoco poco)
        => new CompanyJobProto()
        {
            Id = poco.Id.ToString(),
            Company = poco.Company.ToString(),
            IsCompanyHidden = poco.IsCompanyHidden,
            IsInactive = poco.IsInactive,
            ProfileCreated = poco.ProfileCreated.DateTime2ProtoTimeStamp(),
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
