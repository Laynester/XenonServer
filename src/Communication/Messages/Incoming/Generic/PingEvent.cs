namespace Xenon.Communication.Messages.Incoming.Handshake;

public class PingEvent : IncomingMessage
{
    public required string Yuh { get; set; }
}