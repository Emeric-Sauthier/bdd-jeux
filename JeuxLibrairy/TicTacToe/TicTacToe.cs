using JeuxLibrairy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrairy.TicTacToe
{
    public class TicTacToe
    {
        public char[,] Board { get; private set; }
        public Player? Winner { get; private set; }
        public GameState State { get; private set; }
        public Player? TurnTo { get; private set; }

        public TicTacToe()
        {
            Board = new char[3,3] { 
                { '.', '.', '.' }, 
                { '.', '.', '.' }, 
                { '.', '.', '.' } 
            };
            State = GameState.InProgress;
            TurnTo = Player.Player1;
        }

        public void Play(Player player, int x, int y)
        {

        }
    }
}
