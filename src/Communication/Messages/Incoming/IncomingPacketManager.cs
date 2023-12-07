using System.Text;
using System.Text.Json;
using Xenon.Communication.Clients;
using Xenon.Communication.Handling;
using Xenon.Communication.Messages.Incoming.Generic;
using Xenon.Communication.Messages.Incoming.Handshake;
using Xenon.Utils;

namespace Xenon.Communication.Messages.Incoming;

public class IncomingPacketManager
{
    
    private readonly Logger _logger = new(nameof(IncomingPacketManager));
    
    private readonly Dictionary<string, Type?> _incoming = new();

    private readonly HandlerManager _handlerManager = new();
    
    public IncomingPacketManager()
    {
        RegisterGenericPackets();
        RegisterHandshakePackets();
    }

    private void RegisterGenericPackets()
    {
        RegisterPacket(IncomingMessages.Ping, typeof(PingEvent));
    }
    
    private void RegisterHandshakePackets()
    {
        RegisterPacket(IncomingMessages.AuthTicket, typeof(AuthTicketEvent));
    }

    public void HandlePacket(Client client, IncomingMessage packet, string data)
    {
        try
        {
            Console.WriteLine(data);

            if (!IsRegistered(packet.Header))
            {
                _logger.Debug($"Unhandled packet: {packet.Header}");
                return;
            }

            if (!_incoming.TryGetValue(packet.Header, out var messageType)) return;

            if (messageType == null) return;
            
            var message = (IncomingMessage) JsonSerializer.Deserialize(data, messageType)!;
            
            _handlerManager.Dispatch(client, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public bool IsRegistered(string header)
    {
        return _incoming.ContainsKey(header);
    }
    
    public void RegisterPacket(string header, Type? type)
    {
        _incoming.Add(header, type);
    }

    public static string CleanUp(byte[] bytes)
    {
        var str = Encoding.UTF8.GetString(bytes);

        for (var i = 0; i <= 31; i++)
        {
            str = str.Replace(Convert.ToChar(i).ToString(), "[" + i + "]");
        }

        return str;
    }
    
}