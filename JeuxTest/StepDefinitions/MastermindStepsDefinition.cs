using JeuxLibrary.Common.Enums;
using JeuxLibrary.Mastermind;
using JeuxLibrary.Mastermind.Enums;
using JeuxLibrary.Mastermind.Exceptions;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class MastermindStepsDefinition
    {
        private Mastermind _game;
        private GameStepsContext _context;
        private ProposalResult[]? _proposalResults;

        public MastermindStepsDefinition(GameStepsContext gameStepsContext)
        {
            _context = gameStepsContext;
        }

        [Given("start mastermind game")]
        public void GivenStartGame()
        {
            _game = new Mastermind();
            _context.Game = _game;
        }

        [Given("the secret code is {string}")]
        public void GivenSecretCode(string code)
        {
            try
            {
                _game.SetCode(code);
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }
        }

        [When("the codebreaker proposes {string}")]
        public void WhenDecoderSetsCode(string code)
        {
            Play(code);
        }

        [Then("the result of the proposition should be {string}")]
        public void ThenPropositionResultShouldBe(string result)
        {
            string? resultsToString = string.Join(' ', _proposalResults!.Select(x => x.ToString()));
            Assert.AreEqual(result, resultsToString);
        }

        [Then("the codebreaker should loose")]
        public void ThenCodebreakerShouldLoose()
        {
            Assert.AreEqual(GameState.Lose, _game.State);
            Assert.IsNull(_game.Winner);
        }

        [Then("an error should be throw because one color is invalid")]
        public void ThenErrorShouldBeThrown_InvalidColor()
        {
            Assert.IsInstanceOfType<WrongColorException>(_context.Exception);
        }

        [Then("an error should be throw because the code length is invalid")]
        public void ThenErrorShouldBeThrown_InvalidCodeLength()
        {
            Assert.IsInstanceOfType<InvalidCodeException>(_context.Exception);
        }

        private void Play(string code)
        {
            try
            {
                _context.Exception = null;
                _proposalResults = _game.Play(code);
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }
        }
    }
}
