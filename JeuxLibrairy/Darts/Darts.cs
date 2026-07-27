using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Exceptions;
using JeuxLibrary.Common.Interfaces;
using JeuxLibrary.Darts.Enums;
using JeuxLibrary.Darts.Exceptions;
using JeuxLibrary.Darts.Model;

namespace JeuxLibrary.Darts
{
    public class Darts : IScoredGame
    {
        private const int StartScore = 301;
        private int _dartThrown = 0;
        private int _initialScore = 0;

        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }
        public Dictionary<Player, int> Scores { get; private set; }

        public Darts()
        {
            Scores = new Dictionary<Player, int>() { { Player.Player1, StartScore }, { Player.Player2, StartScore } };
            State = GameState.InProgress;
            TurnTo = Player.Player1;
        }

        public void Play(Player player, int value, Multiplier multiplier)
        {
            if (State != GameState.InProgress)
            {
                throw new GameOverException($"Unable to play, the game is over ({State}).");
            }
            else if (player != TurnTo)
            {
                throw new WrongPlayerException($"{player} cannot play, it is {TurnTo}'s turn.");
            }

            Dart dart = new Dart(value, multiplier);

            if (_dartThrown == 0)
            {
                _initialScore = Scores[player];
            }

            int newScore = Scores[player] - dart.Points;
            _dartThrown++;

            if (newScore < 0 || newScore == 1 || (newScore == 0 && dart.Multiplier != Multiplier.Double))
            {
                Scores[player] = _initialScore;
                SwapTurn();
                return;
            }

            Scores[player] = newScore;

            if (newScore == 0 && dart.Multiplier == Multiplier.Double)
            {
                State = GameState.Win;
                Winner = player;
            }
            else if (_dartThrown == 3)
            {
                SwapTurn();
            }
        }

        public void SetScore(Player player, int score)
        {
            Scores[player] = score;
        }

        public int GetScore(Player player)
        {
            return Scores[player];
        }

        private void SwapTurn()
        {
            TurnTo = (TurnTo == Player.Player1) ? Player.Player2 : Player.Player1;
            _dartThrown = 0;
        }
    }
}
