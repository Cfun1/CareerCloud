using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Mappers;

public static class SystemLanguageCodeMapper
{
    public static SystemLanguageCodePoco[]? ToPoco(this SystemLanguageCodeProtos protos)
    {
        var pocos = new List<SystemLanguageCodePoco>();
        foreach (SystemLanguageCodeProto proto in protos.Proto)
        {
            pocos.Add(new SystemLanguageCodePoco()
            {
                LanguageID = proto.LanguageID,
                Name = proto.Name,
                NativeName = proto.NativeName
            });
        }
        return pocos?.ToArray();
    }

    public static SystemLanguageCodeProtos ToProto(this SystemLanguageCodePoco[] pocos)
    {
        var protos = new SystemLanguageCodeProtos();
        foreach (SystemLanguageCodePoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static SystemLanguageCodeProto ToProto(this SystemLanguageCodePoco poco)
        => new SystemLanguageCodeProto()
        {
            LanguageID = poco.LanguageID,
            Name = poco.Name,
            NativeName = poco.NativeName
        };
}
