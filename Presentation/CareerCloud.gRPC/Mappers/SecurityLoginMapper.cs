using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class SecurityLoginMapper
{
    public static SecurityLoginPoco[]? ToPoco(this SecurityLoginProtos protos)
    {
        var pocos = new List<SecurityLoginPoco>();
        foreach (SecurityLoginProto proto in protos.Proto)
        {
            pocos.Add(new SecurityLoginPoco()
            {
                Id = Guid.Parse(proto.Id),
                AgreementAccepted = proto.AgreementAccepted.ToDateTime(),
                Created = proto.Created.ToDateTime(),
                EmailAddress = proto.EmailAddress,
                ForceChangePassword = proto.ForceChangePassword,
                FullName = proto.FullName,
                IsInactive = proto.IsInactive,
                IsLocked = proto.IsLocked,
                Login = proto.Login,
                Password = proto.Password,
                PasswordUpdate = proto.PasswordUpdate.ToDateTime(),
                PhoneNumber = proto.PhoneNumber,
                PrefferredLanguage = proto.PrefferredLanguage,
                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static SecurityLoginProtos ToProto(this SecurityLoginPoco[] pocos)
    {
        var protos = new SecurityLoginProtos();
        foreach (SecurityLoginPoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static SecurityLoginProto ToProto(this SecurityLoginPoco poco)
        => new SecurityLoginProto()
        {
            Id = poco.Id.ToString(),
            AgreementAccepted = poco.AgreementAccepted.DateTime2ProtoTimeStamp(),
            Created = poco.Created.DateTime2ProtoTimeStamp(),
            EmailAddress = poco.EmailAddress,
            ForceChangePassword = poco.ForceChangePassword,
            FullName = poco.FullName,
            IsInactive = poco.IsInactive,
            IsLocked = poco.IsLocked,
            Login = poco.Login,
            Password = poco.Password,
            PasswordUpdate = poco.PasswordUpdate.DateTime2ProtoTimeStamp(),
            PhoneNumber = poco.PhoneNumber,
            PrefferredLanguage = poco.PrefferredLanguage,
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
