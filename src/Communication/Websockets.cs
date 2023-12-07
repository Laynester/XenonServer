using System.Net;
using System.Net.Sockets;
using Xenon.Communication.Clients;
using Xenon.Communication.Messages;
using NetCoreServer;
using Xenon.Utils;

namespace Xenon.Communication;

public class Websockets : WsServer
{
    private Socket serverSocket;
    private PacketManager _packetManager;
    private Logger _Logger;

    public Websockets(IPAddress address, int port) : base(address, port)
    {
        _Logger = new Logger(GetType().Name);

        this._packetManager = new PacketManager();

        _Logger.Info("Xenon listening on" + port);
    }

    protected override TcpSession CreateSession()
    {
        var client = new Client(this, _packetManager);

        return client;
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
    }
}