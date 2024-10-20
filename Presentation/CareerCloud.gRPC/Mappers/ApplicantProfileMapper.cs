using CareerCloud.Pocos;
using Google.Protobuf;

namespace CareerCloud.gRPC.Mappers;

public static class ApplicantProfileMapper
{
    public static ApplicantProfilePoco[]? ToPoco(this ApplicantProfileProtos protos)
    {
        var pocos = new List<ApplicantProfilePoco>();
        foreach (ApplicantProfileProto proto in protos.Proto)
        {
            pocos.Add(new ApplicantProfilePoco()
            {
                Id = Guid.Parse(proto.Id),
                Login = Guid.Parse(proto.Login),
                City = proto.City,
                Country = proto.Country,
                PostalCode = proto.City,
                Province = proto.City,
                Street = proto.Street,

                /*need more time custom message type to represent decimal in protobuf?*/
                //Currency,
                //CurrentRate,
                //CurrentSalary,

                TimeStamp = proto.TimeStamp.ToByteArray()
            });
        }
        return pocos?.ToArray();
    }

    public static ApplicantProfileProtos ToProto(this ApplicantProfilePoco[] pocos)
    {
        var protos = new ApplicantProfileProtos();
        foreach (ApplicantProfilePoco poco in pocos)
        {
            protos.Proto.Add(poco.ToProto());
        }
        return protos;
    }

    public static ApplicantProfileProto ToProto(this ApplicantProfilePoco poco)
        => new ApplicantProfileProto()
        {
            Id = poco.Id.ToString(),
            Login = poco.Login.ToString(),

            //Currency,
            //CurrentRate,
            //CurrentSalary,
            Country = poco.Country,
            PostalCode = poco.City,
            Province = poco.City,
            Street = poco.Street,
            TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
        };
}
