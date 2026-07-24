using JeuxLibrairy.Common.Enums;
using JeuxLibrairy.Common.Exceptions;
using JeuxLibrary.Darts;
using JeuxLibrary.Darts.Enums;
using JeuxLibrary.Darts.Exceptions;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class DartsStepsDefinition
    {
        private Darts _game;
        private Exception? _exception;

        [Given("start game")]
        public void GivenStartGame()
        {
            _game = new Darts();
        }

        [When("player1 throws a dart and makes a simple {int}")]
        public void WhenPlayer1MakesSimple(int value)
        {
            _game.Play(Player.Player1, value, Multiplier.Simple);
        }

        [When("player1 throws a dart and makes a double {int}")]
        public void WhenPlayer1MakesDouble(int value)
        {
            _game.Play(Player.Player1, value, Multiplier.Double);
        }

        [When("player1 throws a dart and makes a triple {int}")]
        public void WhenPlayer1MakesTriple(int value)
        {
            _game.Play(Player.Player1, value, Multiplier.Triple);
        }

        [When("player1 throws a dart outside of the target")]
        public void WhenPlayer1ThrowsDartOutside()
        {
            _game.Play(Player.Player1, 0, Multiplier.Simple);
        }

        [When("player2 throws a dart and makes a simple {int}")]
        public void WhenPlayer2MakesSimple(int value)
        {
            _game.Play(Player.Player2, value, Multiplier.Simple);
        }

        [When("player2 throws a dart and makes a double {int}")]
        public void WhenPlayer2MakesDouble(int value)
        {
            _game.Play(Player.Player2, value, Multiplier.Double);
        }

        [When("player2 throws a dart and makes a triple {int}")]
        public void WhenPlayer2MakesTriple(int value)
        {
            _game.Play(Player.Player2, value, Multiplier.Triple);
        }

        [When("player2 throws a dart outside of the target")]
        public void WhenPlayer2ThrowsDartOutside()
        {
            _game.Play(Player.Player2, 0, Multiplier.Simple);
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

        [Then("an error should be thrown because the cell is out of grid")]
        public void ThenErrorShouldBeThrown_WrongSector()
        {
            Assert.IsInstanceOfType<WrongDartSector>(_exception);
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
