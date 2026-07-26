using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Exceptions;
using JeuxLibrary.Common.Interfaces;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class GameStepsDefinition
    {
        private readonly GameStepsContext _context;

        public GameStepsDefinition(GameStepsContext gameStepContext)
        {
            _context = gameStepContext;
        }

        [Then("should be turn of player1")]
        public void ThenShouldBeTurnOfPlayer1()
        {
            Assert.AreEqual(Player.Player1, _context.Game!.TurnTo);
        }

        [Then("should be turn of player2")]
        public void ThenShouldBeTurnOfPlayer2()
        {
            Assert.AreEqual(Player.Player2, _context.Game!.TurnTo);
        }

        [Then("player1 should win")]
        public void ThenPlayer1ShouldWin()
        {
            Assert.AreEqual(GameState.Win, _context.Game!.State);
            Assert.AreEqual(Player.Player1, _context.Game!.Winner);
        }

        [Then("player2 should win")]
        public void ThenPlayer2ShouldWin()
        {
            Assert.AreEqual(GameState.Win, _context.Game!.State);
            Assert.AreEqual(Player.Player2, _context.Game!.Winner);
        }

        [Then("game should be a draw")]
        public void ThenGameShouldBeDraw()
        {
            Assert.AreEqual(GameState.Draw, _context.Game!.State);
            Assert.IsNull(_context.Game!.Winner);
        }

        [Then("an error should be thrown because the wrong player tried to play")]
        public void ThenErrorShouldBeThrown_WrongPlayer()
        {
            Assert.IsInstanceOfType<WrongPlayerException>(_context.Exception);
        }

        [Then("an error should be thrown because the game is over")]
        public void ThenErrorShouldBeThrown_GameOver()
        {
            Assert.IsInstanceOfType<GameOverException>(_context.Exception);
        }
    }
}
