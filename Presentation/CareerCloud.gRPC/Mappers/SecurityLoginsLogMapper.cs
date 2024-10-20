using CareerCloud.Pocos;

namespace CareerCloud.gRPC.Mappers;

public static class SecurityLoginsLogMapper
{
    public static SecurityLoginsLogPoco[]? ToPoco(this SecurityLoginsLogProtos protos)
    {
        var pocos = new List<SecurityLoginsLogPoco>();
        foreach (SecurityLoginsLogProto proto in protos.Proto)
        {
            pocos.Add(new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(proto.Id),
                Login = Guid.Parse(proto.Login),
                IsSuccesful = proto.IsSuccesful,
                SourceIP = proto.SourceIP,
                LogonDate = proto.LogonDate.ToDateTime()
            });
        }
        return pocos?.ToArray();
    }

    public static SecurityLoginsLogProtos ToProto(this SecurityLoginsLogPoco[] pocos)
    {
        var protos = new SecurityLoginsLogProtos();
        foreach (SecurityLoginsLogPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static SecurityLoginsLogProto ToProto(this SecurityLoginsLogPoco poco)
        => new SecurityLoginsLogProto()
        {
            Id = poco.Id.ToString(),
            IsSuccesful = poco.IsSuccesful,
            Login = poco.Login.ToString(),
            SourceIP = poco.SourceIP,
            LogonDate = poco.LogonDate.DateTime2ProtoTimeStamp()
        };
}
