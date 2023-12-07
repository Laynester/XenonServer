using System;
using System.Text;
using System.Text.Json;
using Xenon.Communication.Clients;

namespace Xenon.Communication.Messages.Incoming;

public class IncomingMessage
{
    public String message;
    public String Header;
    public Client? client;

    public IIncomingMessage packet;

    public IncomingMessage(String base64)
    {
        this.Deserialize(base64);
    }

    public IncomingMessage(Client client, String base64)
    {
        this.client = client;
        this.Deserialize(base64);
    }

    public void Deserialize(String base64)
    {
        byte[] data = System.Convert.FromBase64String(base64);
        message = ASCIIEncoding.ASCII.GetString(data);
        packet = JsonSerializer.Deserialize<IIncomingMessage>(message);
        Header = packet.header;
    }
}
