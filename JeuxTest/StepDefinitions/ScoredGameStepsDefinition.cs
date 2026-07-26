using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Interfaces;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class ScoredGameStepsDefinition
    {
        private GameStepsContext _context;

        public ScoredGameStepsDefinition(GameStepsContext context)
        {
            _context = context;
        }

        [Given("player1 has a score of {int}")]
        public void GivenPlayer1Score(int score)
        {
            ((IScoredGame)_context.Game!).Scores[Player.Player1] = score;
        }

        [Given("player2 has a score of {int}")]
        public void GivenPlayer2Score(int score)
        {
            ((IScoredGame)_context.Game!).Scores[Player.Player2] = score;
        }

        [Then("player1 should have a score of {int}")]
        public void ThenPlayer1ShouldHaveScoreOf(int score)
        {
            Assert.AreEqual(score, ((IScoredGame)_context.Game!).Scores[Player.Player1]);
        }

        [Then("player2 should have a score of {int}")]
        public void ThenPlayer2ShouldHaveScoreOf(int score)
        {
            Assert.AreEqual(score, ((IScoredGame)_context.Game!).Scores[Player.Player2]);
        }
    }
}
