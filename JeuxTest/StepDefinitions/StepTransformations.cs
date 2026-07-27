using JeuxLibrary.Common.Enums;
using JeuxLibrary.Darts.Enums;

namespace JeuxTest.StepDefinitions
{
    [Binding]
    public class StepTransformations
    {
        [StepArgumentTransformation(@"player(\d)")]
        public Player ToPlayer(int playerNumber)
        {
            if (Enum.IsDefined((Player)playerNumber))
            {
                return (Player)playerNumber;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        [StepArgumentTransformation(@"(simple|double|triple)")]
        public Multiplier ToDartsMultiplier(string multiplier)
        {
            if (Enum.TryParse(multiplier, true, out Multiplier result) && Enum.IsDefined(result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
