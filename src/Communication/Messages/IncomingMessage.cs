namespace Xenon.Communication.Messages.Incoming;

public class IncomingMessage
{
    public required string header { get; set; }
    public required Object data { get; set; }
}