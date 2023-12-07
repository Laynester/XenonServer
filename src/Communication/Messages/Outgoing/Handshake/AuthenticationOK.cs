namespace Xenon.Communication.Messages.Outgoing.Handshake;

public class AuthenticationOK : OutgoingMessage
{
    public AuthenticationOK() : base(OutgoingMessages.AUTHENTICATED) { }
}