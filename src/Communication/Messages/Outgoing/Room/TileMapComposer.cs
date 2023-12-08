namespace Xenon.Communication.Messages.Outgoing.Room;

using Xenon.Communication.Messages.Outgoing;

public class TileMapComposer : OutgoingMessage
{
    public string Map { get; init; }
    public TileMapComposer(string map) : base(OutgoingMessages.TileMap)
    {
        Map = map;
    }
}