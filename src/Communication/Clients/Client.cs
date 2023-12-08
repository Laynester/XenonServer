using System.Net.Sockets;
using System.Text;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Outgoing;
using NetCoreServer;
using System.Text.Json;
using Xenon.Communication.Messages.Outgoing.Core;

namespace Xenon.Communication.Clients;

public class Client : WsSession
{

    private readonly IncomingPacketManager _incomingPacketManager;

    public Client(WsServer server, IncomingPacketManager mgr) : base(server)
    {
        _incomingPacketManager = mgr;
    }

    public override void OnWsConnected(HttpRequest request)
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} connected!");
        SendMessages(new OutgoingMessage[]{
            new ClientConfigComposer(XenonEnvironment.Config().GetClienConfig())
        });
    }

    public override void OnWsDisconnected()
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} disconnected!");
    }

    public override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        var base64 = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        var data = Convert.FromBase64String(base64);
        var message = Encoding.ASCII.GetString(data);
        var packet = JsonSerializer.Deserialize<IncomingMessage>(message);

        _incomingPacketManager.HandlePacket(this, packet!, message);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket session caught an error with code {error}");
    }

    public void SendMessages(OutgoingMessage[] msgs)
    {
        foreach (OutgoingMessage msg in msgs)
        {
            msg.Send(this);
        }
    }

    public void SendMessage(string msg)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(msg);
        string encoded = Convert.ToBase64String(bytes);
        SendText(encoded);
    }

}