using JeuxLibrairy.Common.Enums;
using JeuxLibrairy.Common.Interfaces;
using JeuxLibrary.Darts.Enums;

namespace JeuxLibrary.Darts
{
    public class Darts : IGame
    {
        private const int startScore = 301;

        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }
        public Dictionary<Player, int> Scores { get; private set; }

        public Darts()
        {
            Scores = new Dictionary<Player, int>() { { Player.Player1, startScore }, { Player.Player2, startScore } };
            State = GameState.InProgress;
            TurnTo = Player.Player1;
        }

        public void Play(Player player, int value, Multiplier multiplier)
        {

        }
    }
}
