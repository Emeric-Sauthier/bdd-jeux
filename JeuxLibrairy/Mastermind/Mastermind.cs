using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Exceptions;
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
            if (State != GameState.InProgress)
            {
                throw new GameOverException($"Unable to play, the game is over ({State}).");
            }

            int proposalLength = proposal.Count();
            if (proposalLength != codeLength)
            {
                throw new InvalidCodeException($"Code length is not respected.Should be {codeLength}, given {proposalLength}.");
            }

            int[] secretCodeInt = _secretCode.Select(c => (int)c).ToArray();
            ProposalResult[] results = { ProposalResult.Wrong, ProposalResult.Wrong, ProposalResult.Wrong, ProposalResult.Wrong };
            for (int i = 0; i < codeLength; i++)
            {
                if (secretCodeInt[i] == (int)proposal.ElementAt(i))
                {
                    results[i] = ProposalResult.WellPlaced;
                    secretCodeInt[i] = int.MinValue;
                }
            }

            for (int i = 0; i < codeLength; i++)
            {
                if (results[i] == ProposalResult.WellPlaced)
                {
                    continue;
                }

                int index = secretCodeInt.ToList().IndexOf((int)proposal.ElementAt(i));
                if (index != -1)
                {
                    results[i] = ProposalResult.Misplaced;
                    secretCodeInt[index] = int.MinValue;
                }
            }

            bool isWin = results.Count(r => r == ProposalResult.WellPlaced) == codeLength;
            if (isWin)
            {
                State = GameState.Win;
                Winner = Player.Player1;
            }
            else if (Round == maxRound)
            {
                State = GameState.Lose;
            }
            else
            {
                Round++;
            }

            return results;
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
                }
                else
                {
                    throw new WrongColorException($"Unknown color '{stringColor}'.");
                }
            }

            return colors;
        }
    }
}
