namespace Xenon.Communication.Messages.Outgoing;

using System.Text;
using System.Text.Json;
using Xenon.Communication.Clients;

public class OutgoingMessage
{

    public string Header { get; init; }

    public OutgoingMessage(string header)
    {
        Header = header;
    }

    public void Send(Client client)
    {
        client.SendMessage(JsonSerializer.Serialize(this, GetType()));
    }
}
