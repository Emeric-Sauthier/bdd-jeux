using JeuxLibrary.Common.Enums;
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

        [When(@"^(player\d) throws a dart and makes a (simple|double|triple) (\d+)$")]
        public void WhenPlayerThrowsDart(Player player, Multiplier multiplier, int sector)
        {
            Play(player, sector, multiplier);
        }

        [When(@"^(player\d) throws a dart outside of the target$")]
        public void WhenPlayerMissesDart(Player player)
        {
            Play(player, 0, Multiplier.Simple);
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
                _context.Exception = null;
                _game.Play(player, value, multiplier);
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }
        }
    }
}
