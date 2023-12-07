using Xenon.Communication.Messages.Incoming;

namespace Xenon.Communication.Utils;

public class Handler
{
    private Dictionary<string, Action<IncomingMessage>> _Listeners = new();

    public void On(string header, Action<IncomingMessage> callback)
    {
        _Listeners[header] = callback;
    }

    public void Dispatch(IncomingMessage msg)
    {
        if (!_Listeners.ContainsKey(msg.Header)) return;

        var callback = _Listeners[msg.Header];

        if (callback == null) return;

        callback(msg);
    }
}