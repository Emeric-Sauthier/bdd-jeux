using JeuxLibrairy.Common.Enums;
using JeuxLibrairy.Common.Exceptions;
using JeuxLibrairy.TicTacToe.Exceptions;

namespace JeuxLibrairy.TicTacToe
{
    public class TicTacToe
    {
        private const int boardDimension = 3;
        private const char blankCellChar = '.';

        private readonly Dictionary<Player, char> playerChar = new Dictionary<Player, char>() { {Player.Player1, 'X'} , {Player.Player2, 'O'} };

        public char[,] Board { get; private set; }
        public Player? Winner { get; private set; }
        public GameState State { get; private set; }
        public Player TurnTo { get; private set; }

        public TicTacToe()
        {
            Board = new char[3,3] { 
                { blankCellChar, blankCellChar, blankCellChar }, 
                { blankCellChar, blankCellChar, blankCellChar }, 
                { blankCellChar, blankCellChar, blankCellChar } 
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
            else if (x < 0 || x >= boardDimension || y < 0 || y >= boardDimension)
            {
                throw new CellOutOfGridException($"The cell ({x},{y}) is out of the grid.");
            }
            else if (Board[x,y] != blankCellChar)
            {
                throw new CellOccupiedException($"Unable to put '{playerChar[player]}' at ({x},{y}), already occupied.");
            }

            Board[x,y] = playerChar[player];

            CheckBoard();
            SwapTurn();
        }

        private void CheckBoard()
        {
            if (CheckDiagonalWin())
            {
                Winner = TurnTo;
                State = GameState.Win;
                return;
            }

            int blankCharacters = 0;
            for (int i = 0; i < boardDimension; i++)
            {
                bool winOnRow = true;
                bool winOnColumn = true;

                for (int j = 0; j < boardDimension; j++)
                {
                    winOnRow = winOnRow & Board[i,j] == playerChar[TurnTo];
                    winOnColumn = winOnColumn & Board[j,i] == playerChar[TurnTo];

                    if (Board[i, j] == blankCellChar)
                    {
                        blankCharacters++;
                    }
                }

                if (winOnRow || winOnColumn)
                {
                    Winner = TurnTo;
                    State = GameState.Win;
                    return;
                }
            }

            if (blankCharacters == 0)
            {
                State = GameState.Draw;
                return;
            }
        }

        private bool CheckDiagonalWin()
        {
            return (Board[0,0] == playerChar[TurnTo] && Board[1,1] == playerChar[TurnTo] && Board[2,2] == playerChar[TurnTo]) 
                || (Board[0,2] == playerChar[TurnTo] && Board[1,1] == playerChar[TurnTo] && Board[2,0] == playerChar[TurnTo]);
        }

        private void SwapTurn()
        {
            TurnTo = (TurnTo == Player.Player1) ? Player.Player2 : Player.Player1;
        }
    }
}
