namespace CareerCloud.Pocos;

public interface IRowVersion
{
    byte[] TimeStamp { get; set; }
}