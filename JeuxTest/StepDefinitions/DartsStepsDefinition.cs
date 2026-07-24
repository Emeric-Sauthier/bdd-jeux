using JeuxLibrairy.Common.Enums;
using JeuxLibrary.Darts;
using JeuxLibrary.Darts.Enums;
using JeuxLibrary.Darts.Exceptions;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class DartsStepsDefinition
    {
        private Darts _game;
        private GameStepsContext _context;

        public DartsStepsDefinition(GameStepsContext gameStepsContext)
        {
            _context = gameStepsContext;
        }

        [Given("start darts game")]
        public void GivenStartGame()
        {
            _game = new Darts();
            _context.Game = _game;
        }

        [Given("player1 has a score of {int}")]
        public void GivenPlayer1Score(int score)
        {
            _game.Scores[Player.Player1] = score;
        }

        [Given("player2 has a score of {int}")]
        public void GivenPlayer2Score(int score)
        {
            _game.Scores[Player.Player2] = score;
        }

        [When("player1 throws a dart and makes a simple {int}")]
        public void WhenPlayer1MakesSimple(int value)
        {
            Play(Player.Player1, value, Multiplier.Simple);
        }

        [When("player1 throws a dart and makes a double {int}")]
        public void WhenPlayer1MakesDouble(int value)
        {
            Play(Player.Player1, value, Multiplier.Double);
        }

        [When("player1 throws a dart and makes a triple {int}")]
        public void WhenPlayer1MakesTriple(int value)
        {
            Play(Player.Player1, value, Multiplier.Triple);
        }

        [When("player1 throws a dart outside of the target")]
        public void WhenPlayer1ThrowsDartOutside()
        {
            Play(Player.Player1, 0, Multiplier.Simple);
        }

        [When("player2 throws a dart and makes a simple {int}")]
        public void WhenPlayer2MakesSimple(int value)
        {
            Play(Player.Player2, value, Multiplier.Simple);
        }

        [When("player2 throws a dart and makes a double {int}")]
        public void WhenPlayer2MakesDouble(int value)
        {
            Play(Player.Player2, value, Multiplier.Double);
        }

        [When("player2 throws a dart and makes a triple {int}")]
        public void WhenPlayer2MakesTriple(int value)
        {
            Play(Player.Player2, value, Multiplier.Triple);
        }

        [When("player2 throws a dart outside of the target")]
        public void WhenPlayer2ThrowsDartOutside()
        {
            Play(Player.Player2, 0, Multiplier.Simple);
        }

        [Then("player1 should have a score of {int}")]
        public void ThenPlayer1ShouldHaveScoreOf(int score)
        {
            Assert.AreEqual(score, _game.Scores[Player.Player1]);
        }

        [Then("player2 should have a score of {int}")]
        public void ThenPlayer2ShouldHaveScoreOf(int score)
        {
            Assert.AreEqual(score, _game.Scores[Player.Player2]);
        }

        [Then("an error should be thrown because the sector is invalid")]
        public void ThenErrorShouldBeThrown_WrongSector()
        {
            Assert.IsInstanceOfType<WrongDartSectorException>(_context.Exception);
        }

        [Then("an error should be thrown because the multiplier is invalid")]
        public void ThenErrorShouldBeThrown_WrongMultiplier()
        {
            Assert.IsInstanceOfType<WrongDartMultiplierException>(_context.Exception);
        }

        private void Play(Player player, int value, Multiplier multiplier)
        {
            try
            {
                _game.Play(player, value, multiplier);
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }
        }
    }
}
