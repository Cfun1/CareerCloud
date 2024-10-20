using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Mappers;

public static class Mappers
{
    public static Timestamp? DateTime2ProtoTimeStamp(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        return ((DateTime)dateTime).DateTime2ProtoTimeStamp();
    }

    public static Timestamp? DateTime2ProtoTimeStamp(this DateTime dateTime)
    {
        var tempDateTimeUtc = ((DateTime)dateTime).ToUniversalTime();
        return Timestamp.FromDateTime(tempDateTimeUtc);
    }
}
//uint protobufValue = nullableByte.HasValue ? nullableByte.Value : 0; // Assuming 0 indicates null
//    Value = nullableByte.HasValue ? new Google.Protobuf.UInt32Value { Value = nullableByte.Value } : null
