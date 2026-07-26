using JeuxLibrary.Common.Enums;

namespace JeuxLibrary.Common.Interfaces
{
    public interface IGame
    {
        GameState State { get; }
        Player? Winner { get; }
        Player TurnTo { get; }
    }
}
