using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Handshake;
using Xenon.Communication.Utils;

namespace Xenon.Communication.Handlers;

public class HandshakeHandler : Handler
{
    public HandshakeHandler()
    {
        On(IncomingMessages.AUTH_TICKET, (msg) => OnReleaseVersion(msg));
    }

    private void OnReleaseVersion(ClientHelloEvent evt)
    {
        Console.WriteLine(evt._Build);
    }
}