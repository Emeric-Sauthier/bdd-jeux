using JeuxLibrary.Common.Interfaces;
using JeuxLibrary.Mastermind;
using JeuxTest.Support;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public sealed class MastermindStepsDefinition
    {
        private Mastermind _game;
        private GameStepsContext _context;

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

        private void Play()
        {
            try
            {
                _game.Play();
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }
        }
    }
}
