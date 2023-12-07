using System.Net.Sockets;
using System.Text;
using Xenon.Communication.Messages;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Outgoing;
using NetCoreServer;
using System.Text.Json;

namespace Xenon.Communication.Clients;

public class Client : WsSession
{
    private PacketManager _packetManager;
    public Client(WsServer server, PacketManager mgr) : base(server)
    {
        this._packetManager = mgr;
    }

    public override void OnWsConnected(HttpRequest request)
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} connected!");
    }

    public override void OnWsDisconnected()
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} disconnected!");
    }

    public override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        string base64 = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        byte[] data = System.Convert.FromBase64String(base64);
        string message = ASCIIEncoding.ASCII.GetString(data);
        IncomingMessage packet = JsonSerializer.Deserialize<IncomingMessage>(message);
        string = packet.header;
        _packetManager.HandlePacket(this, packet, message);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket session caught an error with code {error}");
    }

    public void SendMessage(OutgoingMessage msg)
    {
        byte[] buffer = msg.Compose();
        SendBinaryAsync(buffer, 0, buffer.Length);
    }
}