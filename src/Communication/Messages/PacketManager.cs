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

            if (client == null || !IsRegistered(packet.header)) return;

            Type messageType = _Incoming[packet.header];

            IncomingMessage? message = JsonSerializer.Deserialize < messageType.GetType() > (data);

            if (message == null) return;

            //_HandlerManager.Dispatch(JsonSerializer.Deserialize<IncomingMessage>(packet.message));
        }
        catch (System.Exception)
        {
            //ignored for now
        }
    }

    public bool IsRegistered(string header)
    {
        return _Incoming.ContainsKey(header);
    }

    public void RegisterPackets()
    {
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