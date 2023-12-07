using System.Reflection;
using System.Text;
using System.Text.Json;
using Xenon.Communication.Clients;
using Xenon.Communication.Handlers;
using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Messages.Incoming.Handshake;

namespace Xenon.Communication.Messages;

public class PacketManager
{
    private readonly Dictionary<string, Type> _Incoming = new();

    public readonly HandlerManager _HandlerManager = new();
    public PacketManager()
    {
        RegisterPackets();
    }

    public void HandlePacket(Client client, IncomingMessage packet, string data)
    {
        try
        {
            Console.WriteLine(data);

            if (client == null || !IsRegistered(packet.Header)) return;

            if (_Incoming.TryGetValue(packet.Header, out Type messageType))
            {
                IncomingMessage? message = (IncomingMessage)JsonSerializer.Deserialize(data, messageType);
                _HandlerManager.Dispatch(message);
            }

        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public bool IsRegistered(string header)
    {
        return _Incoming.ContainsKey(header);
    }

    public void RegisterPackets()
    {
        _Incoming.Add(IncomingMessages.PING, typeof(PingEvent));
        _Incoming.Add(IncomingMessages.AUTH_TICKET, typeof(AuthTicketEvent));
    }

    public string CleanUp(byte[] bytes)
    {
        string str = Encoding.UTF8.GetString(bytes);

        for (int i = 0; i <= 31; i++)
        {
            str = str.Replace(Convert.ToChar(i).ToString(), "[" + i + "]");
        }

        return str;
    }
}