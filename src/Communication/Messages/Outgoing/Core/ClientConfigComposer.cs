namespace Xenon.Communication.Messages.Outgoing.Core;

using Xenon.Communication.Messages.Outgoing;

public class ClientConfigComposer : OutgoingMessage
{
    public Dictionary<string, string> Values { get; init; }
    public ClientConfigComposer(Dictionary<string, string> config) : base(OutgoingMessages.ClientConfig)
    {
        Values = config;
    }
}