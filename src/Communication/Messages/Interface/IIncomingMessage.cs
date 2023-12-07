using System;
using Xenon.Communication.Clients;

namespace Xenon.Communication.Messages.Incoming;

public class IIncomingMessage
{
    public required string header { get; set; }
    public required Object data { get; set; }
}