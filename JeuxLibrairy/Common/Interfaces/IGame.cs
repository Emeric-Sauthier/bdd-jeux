using JeuxLibrairy.Common.Enums;

namespace JeuxLibrairy.Common.Interfaces
{
    public interface IGame
    {
        GameState State { get; }
        Player? Winner { get; }
        Player TurnTo { get; }
    }
}
