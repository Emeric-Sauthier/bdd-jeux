using JeuxLibrary.Common.Enums;
using JeuxLibrary.TicTacToe;
using JeuxLibrary.TicTacToe.Exceptions;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class TicTacToeStepsDefinitions
    {
        private GameStepsContext _context;
        private TicTacToe _game;

        public TicTacToeStepsDefinitions(GameStepsContext gameStepsContext)
        {
            _context = gameStepsContext;
        }

        [Given("start tic tac toe game")]
        public void GivenStartGame()
        {
            _game = new TicTacToe();
            _context.Game = _game;
        }

        [When(@"player1 puts 'X' at \({int},{int}\)")]
        public void WhenPlayer1Play(int x, int y)
        {
            Play(Player.Player1, x, y);
        }

        [When(@"player2 puts 'O' at \({int},{int}\)")]
        public void WhenPlayer2Play(int x, int y)
        {
            Play(Player.Player2, x, y);
        }

        [Then("an error should be thrown because the cell is already occupied")]
        public void ThenErrorShouldBeThrown_CellOccupied()
        {
            Assert.IsInstanceOfType<CellOccupiedException>(_context.Exception);
        }

        [Then("an error should be thrown because the cell is out of grid")]
        public void ThenErrorShouldBeThrown_CellOutOfGrid()
        {
            Assert.IsInstanceOfType<CellOutOfGridException>(_context.Exception);
        }

        private void Play(Player player, int x, int y)
        {
            try
            {
                _context.Exception = null;
                _game.Play(player, x, y);
            }
            catch (Exception ex)
            {
                _context.Exception = ex;
            }
        }
    }
}
