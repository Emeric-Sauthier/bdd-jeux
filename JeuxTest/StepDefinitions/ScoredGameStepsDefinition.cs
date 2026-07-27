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

        [Given(@"^(player\d) has a score of (\d+)$")]
        public void GivenPlayerScore(Player player, int score)
        {
            ((IScoredGame)_context.Game!).SetScore(player, score);
        }

        [Then(@"^(player\d) should have a score of (\d+)$")]
        public void ThenPlayerShouldHaveScoreOf(Player player, int score)
        {
            int playerScore = ((IScoredGame)_context.Game!).GetScore(player);
            Assert.AreEqual(score, playerScore);
        }
    }
}
