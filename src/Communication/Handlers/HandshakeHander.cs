using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Handshake;
using Xenon.Communication.Utils;

namespace Xenon.Communication.Handlers;

public class HandshakeHandler : Handler
{
    public HandshakeHandler()
    {
        On(IncomingMessages.AUTH_TICKET, (msg) => OnAuthTicket((AuthTicketEvent)msg));
        On(IncomingMessages.PING, (msg) => OnPing((PingEvent)msg));
    }

    private void OnPing(PingEvent evt)
    {
        Console.WriteLine(evt.Yuh);
    }

    private void OnAuthTicket(AuthTicketEvent evt)
    {
        Console.WriteLine(evt.Ticket);
    }
}