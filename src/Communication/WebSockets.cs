using System.Net;
using System.Net.Sockets;
using Xenon.Communication.Clients;
using Xenon.Communication.Messages;
using NetCoreServer;
using Xenon.Communication.Messages.Incoming;
using Xenon.Utils;

namespace Xenon.Communication;

public class WebSockets : WsServer
{

    private readonly IncomingPacketManager _incomingPacketManager;
    private readonly Logger _logger = new(nameof(WebSockets));

    public WebSockets(IPAddress address, int port) : base(address, port)
    {
        _incomingPacketManager = new IncomingPacketManager();

        _logger.Info($"Listening for WebSocket connections on {address}:{port}");
    }

    protected override TcpSession CreateSession()
    {
        var client = new Client(this, _incomingPacketManager);

        return client;
    }

    protected override void OnError(SocketError error)
    {
        // unhandle
    }

}