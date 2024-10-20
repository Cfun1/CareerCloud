using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class CompanyDescriptionMapper
{
    public static CompanyDescriptionPoco[]? ToPoco(this CompanyDescriptionProtos protos)
    {
        var pocos = new List<CompanyDescriptionPoco>();
        foreach (CompanyDescriptionProto proto in protos.Proto)
        {
            pocos.Add(new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(proto.Id),
                Company = Guid.Parse(proto.Company),
                CompanyDescription = proto.CompanyDescription,
                CompanyName = proto.CompanyName,
                LanguageId = proto.LanguageId,
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static CompanyDescriptionProtos ToProto(this CompanyDescriptionPoco[] pocos)
    {
        var protos = new CompanyDescriptionProtos();
        foreach (CompanyDescriptionPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static CompanyDescriptionProto ToProto(this CompanyDescriptionPoco poco)
        => new CompanyDescriptionProto()
        {
            Id = poco.Id.ToString(),
            Company = poco.Company.ToString(),
            CompanyDescription = poco.CompanyDescription,
            CompanyName = poco.CompanyName,
            LanguageId = poco.LanguageId,
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
