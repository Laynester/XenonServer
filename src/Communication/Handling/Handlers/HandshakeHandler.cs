using System.Text.Json;
using Xenon.Communication.Clients;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Generic;
using Xenon.Communication.Messages.Incoming.Handshake;
using Xenon.Communication.Messages.Outgoing.Room;

namespace Xenon.Communication.Handling.Handlers;

public class HandshakeHandler : Handler
{

    public HandshakeHandler()
    {
        On(IncomingMessages.AuthTicket, (client, msg) => OnAuthTicket(client, (AuthTicketEvent)msg));
    }

    private static void OnAuthTicket(Client client, AuthTicketEvent evt)
    {
        Console.WriteLine(evt.Ticket);
    }

}