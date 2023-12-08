using System.Text.Json;
using Xenon.Communication.Clients;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Generic;
using Xenon.Communication.Messages.Incoming.Handshake;
using Xenon.Communication.Messages.Outgoing.Room;

namespace Xenon.Communication.Handling.Handlers;

public class GenericHandler : Handler
{

    public GenericHandler()
    {
        On(IncomingMessages.Ping, (client, msg) => OnPing(client, (PingEvent)msg));
        On(IncomingMessages.RequestDesktop, (client, msg) => OnRequestDesktop(client, (RequestDesktopEvent)msg));
    }

    private static void OnPing(Client client, PingEvent evt)
    {
        var map = @"
        xxxxxxxxxxxxxxxxxxxxxxxxxx
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000
        x0000000000000000000000000";

        new TileMapComposer(map).Send(client);
    }

    private static void OnRequestDesktop(Client client, RequestDesktopEvent evt)
    {
        Console.WriteLine("desktop");
    }
}