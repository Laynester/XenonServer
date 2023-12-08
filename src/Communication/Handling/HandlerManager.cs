using Xenon.Communication.Clients;
using Xenon.Communication.Handling.Handlers;
using Xenon.Communication.Messages.Incoming;

namespace Xenon.Communication.Handling;

public class HandlerManager
{

    private readonly List<Handler> _handlers = new();

    public HandlerManager()
    {
        _handlers.Add(new HandshakeHandler());
        _handlers.Add(new GenericHandler());
    }

    public void Dispatch(Client client, IncomingMessage msg)
    {
        foreach (var handler in _handlers)
            handler.Dispatch(client, msg);
    }

}