namespace Xenon.Communication.Messages.Outgoing.Handshake;

public class AuthenticationOk : OutgoingMessage
{
    public AuthenticationOk() : base(OutgoingMessages.AUTHENTICATED) { }
}