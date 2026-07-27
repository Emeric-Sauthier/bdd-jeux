using JeuxLibrary.Common.Enums;

namespace JeuxLibrary.Common.Interfaces
{
    public interface IScoredGame : IGame
    {
        Dictionary<Player, int> Scores { get; }

        void SetScore(Player player, int score);
        int GetScore(Player player);
    }
}
