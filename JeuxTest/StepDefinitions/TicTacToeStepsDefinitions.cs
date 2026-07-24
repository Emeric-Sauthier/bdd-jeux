using JeuxLibrairy.Common.Enums;
using JeuxLibrairy.Common.Exceptions;
using JeuxLibrairy.TicTacToe;
using JeuxLibrairy.TicTacToe.Exceptions;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class TicTacToeStepsDefinitions
    {
        private TicTacToe _game;
        private Exception? _exception;

        [Given("start game")]
        public void GivenStartGame()
        {
            _game = new TicTacToe();
        }

        [When(@"player1 puts 'X' at \({int},{int}\)")]
        public void WhenPlayer1Play(int x, int y)
        {
            try
            {
                _game.Play(Player.Player1, x, y);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [When(@"player2 puts 'O' at \({int},{int}\)")]
        public void WhenPlayer2Play(int x, int y)
        {
            try
            {
                _game.Play(Player.Player2, x, y);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then("should be turn of player1")]
        public void ThenShouldBeTurnOfPlayer1()
        {
            Assert.AreEqual(Player.Player1, _game.TurnTo);
        }

        [Then("should be turn of player2")]
        public void ThenShouldBeTurnOfPlayer2()
        {
            Assert.AreEqual(Player.Player2, _game.TurnTo);
        }

        [Then("player1 should win")]
        public void ThenPlayer1ShouldWin()
        {
            Assert.AreEqual(GameState.Win, _game.State);
            Assert.AreEqual(Player.Player1, _game.Winner);
        }

        [Then("player2 should win")]
        public void ThenPlayer2ShouldWin()
        {
            Assert.AreEqual(GameState.Win, _game.State);
            Assert.AreEqual(Player.Player2, _game.Winner);
        }

        [Then("game should be a draw")]
        public void ThenGameShouldBeDraw()
        {
            Assert.AreEqual(GameState.Draw, _game.State);
            Assert.IsNull(_game.Winner);
        }

        [Then("an error should be thrown because the cell is already occupied")]
        public void ThenErrorShouldBeThrown_CellOccupied()
        {
            Assert.IsInstanceOfType<CellOccupiedException>(_exception);
        }

        [Then("an error should be thrown because the cell is out of grid")]
        public void ThenErrorShouldBeThrown_CellOutOfGrid()
        {
            Assert.IsInstanceOfType<CellOutOfGridException>(_exception);
        }

        [Then("an error should be thrown because the wrong player tried to play")]
        public void ThenErrorShouldBeThrown_WrongPlayer()
        {
            Assert.IsInstanceOfType<WrongPlayerException>(_exception);
        }

        [Then("an error should be thrown because the game is over")]
        public void ThenErrorShouldBeThrown_GameOver()
        {
            Assert.IsInstanceOfType<GameOverException>(_exception);
        }
    }
}
