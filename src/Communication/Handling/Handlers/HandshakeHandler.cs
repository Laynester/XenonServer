using Xenon.Communication.Clients;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Generic;
using Xenon.Communication.Messages.Incoming.Handshake;

namespace Xenon.Communication.Handling.Handlers;

public class HandshakeHandler : Handler
{
    
    public HandshakeHandler()
    {
        On(IncomingMessages.AuthTicket, (client, msg) => OnAuthTicket(client, (AuthTicketEvent) msg));
        On(IncomingMessages.Ping, (client, msg) => OnPing(client, (PingEvent) msg));
    }

    private static void OnPing(Client client, PingEvent evt)
    {
        Console.WriteLine(evt.Yuh);
    }

    private static void OnAuthTicket(Client client, AuthTicketEvent evt)
    {
        Console.WriteLine(evt.Ticket);
    }
    
}