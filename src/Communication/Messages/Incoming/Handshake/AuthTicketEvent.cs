namespace Xenon.Communication.Messages.Incoming.Handshake;

public class AuthTicketEvent : IncomingMessage
{
    public required string Ticket { get; set; }
}