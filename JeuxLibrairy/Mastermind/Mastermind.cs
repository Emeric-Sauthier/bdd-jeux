using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Interfaces;

namespace JeuxLibrary.Mastermind
{
    public class Mastermind : IScoredGame
    {
        private const int maxRound = 10;

        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }
        public Dictionary<Player, int> Scores { get; private set; }

        public Mastermind()
        {
            State = GameState.InProgress;
            TurnTo = Player.Player1;
        }

        public void Play()
        {

        }
    }
}
