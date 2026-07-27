using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Exceptions;
using JeuxLibrary.Common.Interfaces;
using JeuxLibrary.TicTacToe.Exceptions;

namespace JeuxLibrary.TicTacToe
{
    public class TicTacToe : IGame
    {
        private const int BoardDimension = 3;
        private const char BlankCellChar = '.';

        private readonly Dictionary<Player, char> playerChar = new Dictionary<Player, char>() { { Player.Player1, 'X' }, { Player.Player2, 'O' } };

        public char[,] Board { get; private set; }
        public Player? Winner { get; private set; }
        public GameState State { get; private set; }
        public Player TurnTo { get; private set; }

        public TicTacToe()
        {
            Board = new char[3, 3] {
                { BlankCellChar, BlankCellChar, BlankCellChar },
                { BlankCellChar, BlankCellChar, BlankCellChar },
                { BlankCellChar, BlankCellChar, BlankCellChar }
            };
            State = GameState.InProgress;
            TurnTo = Player.Player1;
        }

        public void Play(Player player, int x, int y)
        {
            if (State != GameState.InProgress)
            {
                throw new GameOverException($"Unable to play, the game is over ({State}).");
            }
            else if (player != TurnTo)
            {
                throw new WrongPlayerException($"{player} cannot play, it is {TurnTo}'s turn.");
            }
            else if (x < 0 || x >= BoardDimension || y < 0 || y >= BoardDimension)
            {
                throw new CellOutOfGridException($"The cell ({x},{y}) is out of the grid.");
            }
            else if (Board[x, y] != BlankCellChar)
            {
                throw new CellOccupiedException($"Unable to put '{playerChar[player]}' at ({x},{y}), already occupied.");
            }

            Board[x, y] = playerChar[player];

            if (IsDiagonalWin(playerChar[player]) || IsRowAndColumnWin(playerChar[player]))
            {
                Winner = player;
                State = GameState.Win;
                return;
            }

            if (IsDraw())
            {
                State = GameState.Draw;
                return;
            }

            SwapTurn();
        }

        private bool IsRowAndColumnWin(char charToCheck)
        {
            for (int i = 0; i < BoardDimension; i++)
            {
                bool winOnRow = true;
                bool winOnColumn = true;

                for (int j = 0; j < BoardDimension; j++)
                {
                    winOnRow = winOnRow & Board[i, j] == charToCheck;
                    winOnColumn = winOnColumn & Board[j, i] == charToCheck;
                }

                if (winOnRow || winOnColumn)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsDiagonalWin(char charToCheck)
        {
            return Board[0, 0] == charToCheck && Board[1, 1] == charToCheck && Board[2, 2] == charToCheck
                || Board[0, 2] == charToCheck && Board[1, 1] == charToCheck && Board[2, 0] == charToCheck;
        }

        private bool IsDraw()
        {
            foreach (char c in Board)
            {
                if (c == BlankCellChar)
                {
                    return false;
                }
            }

            return true;
        }

        private void SwapTurn()
        {
            TurnTo = TurnTo == Player.Player1 ? Player.Player2 : Player.Player1;
        }
    }
}
