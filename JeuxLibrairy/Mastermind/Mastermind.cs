using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Interfaces;
using JeuxLibrary.Mastermind.Enums;
using JeuxLibrary.Mastermind.Exceptions;

namespace JeuxLibrary.Mastermind
{
    public class Mastermind : IGame
    {
        private const int maxRound = 10;
        private const int codeLength = 4;
        private Color[] _secretCode = new Color[codeLength];

        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }
        public int Round { get; private set; }

        public Mastermind()
        {
            State = GameState.InProgress;
            TurnTo = Player.Player1;
            Round = 1;
        }

        public ProposalResult[] Play(string code)
        {
            IEnumerable<Color> colors = ParseCombinaison(code);
            return Play(colors);
        }
        public ProposalResult[] Play(IEnumerable<Color> proposal)
        {
            int proposalLength = proposal.Count();
            if (proposalLength != codeLength)
            {
                throw new InvalidCodeException($"Code length is not respected.Should be {codeLength}, given {proposalLength}.");
            }

            return new ProposalResult[codeLength];
        }

        public void SetCode(string code)
        {
            IEnumerable<Color> colors = ParseCombinaison(code);
            SetCode(colors);
        }
        public void SetCode(IEnumerable<Color> code)
        {
            int colorCount = code.Count();
            if (colorCount != codeLength)
            {
                throw new InvalidCodeException($"Code length is not respected. Should be {codeLength}, given {colorCount}.");
            }

            _secretCode = code.ToArray();
        }

        private IEnumerable<Color> ParseCombinaison(string combinaison)
        {
            IEnumerable<string> stringColors = combinaison.Split(' ').Select(x => x.Trim());
            List<Color> colors = new List<Color>();

            foreach (string stringColor in stringColors)
            {
                if (Color.TryParse(stringColor, out Color color))
                {
                    colors.Add(color);
                } else
                {
                    throw new WrongColorException($"Unknown color '{stringColor}'.");
                }
            }

            return colors;
        }
    }
}
