using JeuxLibrairy.Common.Enums;
using JeuxLibrairy.Common.Interfaces;
using JeuxLibrary.Darts.Enums;

namespace JeuxLibrary.Darts
{
    public class Darts : IGame
    {
        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }

        public Darts() { }

        public void Play(Player player, int value, Multiplier multiplier)
        {

        }
    }
}
