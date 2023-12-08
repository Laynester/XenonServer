using Xenon.Communication.Clients;
using Xenon.Communication.Messages.Incoming;

namespace Xenon.Communication.Handling;

public class Handler
{

    private readonly Dictionary<string, Action<Client, IncomingMessage>> _listeners = new();

    protected void On(string header, Action<Client, IncomingMessage> callback)
    {
        _listeners[header] = callback;
    }

    public void Dispatch(Client client, IncomingMessage msg)
    {
        if (!_listeners.ContainsKey(msg.Header)) return;

        var callback = _listeners[msg.Header];

        callback(client, msg);
    }

}