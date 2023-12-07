using Xenon.Communication.Messages.Incoming;
using Xenon.Communication.Utils;

namespace Xenon.Communication.Handlers;

public class HandlerManager
{
    private readonly List<Handler> _Handlers = new();
    public HandlerManager()
    {
        _Handlers.Add(new HandshakeHandler());
    }

    public void Dispatch(IncomingMessage msg)
    {
        foreach (Handler handler in _Handlers)
            handler.Dispatch(msg);
    }
}