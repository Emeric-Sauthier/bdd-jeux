using JeuxLibrary.Common.Enums;
using JeuxLibrary.Common.Exceptions;
using JeuxLibrary.Common.Interfaces;
using JeuxLibrary.Mastermind.Enums;
using JeuxLibrary.Mastermind.Exceptions;

namespace JeuxLibrary.Mastermind
{
    public class Mastermind : IGame
    {
        private const int MaxRound = 10;
        private const int CodeLength = 4;
        private Color[]? _secretCode;

        public GameState State { get; private set; }
        public Player? Winner { get; private set; }
        public Player TurnTo { get; private set; }
        public int Round { get; private set; }

        public Mastermind()
        {
            State = GameState.Pending;
            TurnTo = Player.Player1;
            Round = 1;
        }
        public Mastermind(IEnumerable<Color> secretCode) : this()
        {
            SetCode(secretCode);
        }

        public ProposalResult[] Play(string code)
        {
            IEnumerable<Color> colors = ParseCombinaison(code);
            return Play(colors);
        }
        public ProposalResult[] Play(IEnumerable<Color> proposal)
        {
            if (_secretCode is null)
            {
                throw new NoSecretCodeException("Unable to play, any secret code is defined.");
            }
            else if (State != GameState.InProgress)
            {
                throw new GameOverException($"Unable to play, the game is over ({State}).");
            }

            ValidateCombinaison(proposal);

            int[] secretCodeInt = _secretCode.Select(c => (int)c).ToArray();
            ProposalResult[] results = { ProposalResult.Wrong, ProposalResult.Wrong, ProposalResult.Wrong, ProposalResult.Wrong };
            for (int i = 0; i < CodeLength; i++)
            {
                if (secretCodeInt[i] == (int)proposal.ElementAt(i))
                {
                    results[i] = ProposalResult.WellPlaced;
                    secretCodeInt[i] = int.MinValue;
                }
            }

            for (int i = 0; i < CodeLength; i++)
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

            bool isWin = results.Count(r => r == ProposalResult.WellPlaced) == CodeLength;
            if (isWin)
            {
                State = GameState.Win;
                Winner = Player.Player1;
            }
            else if (Round == MaxRound)
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
            if (State != GameState.Pending)
            {
                throw new InvalidOperationException("Cannot set code at this moment of the game.");
            }

            ValidateCombinaison(code);

            _secretCode = code.ToArray();
            State = GameState.InProgress;
        }

        private IEnumerable<Color> ParseCombinaison(string combinaison)
        {
            IEnumerable<string> stringColors = combinaison.Split(' ').Select(x => x.Trim());
            List<Color> colors = new List<Color>();

            foreach (string stringColor in stringColors)
            {
                if (Color.TryParse(stringColor, out Color color) && Enum.IsDefined(color))
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

        private void ValidateCombinaison(IEnumerable<Color> colors)
        {
            int colorCount = colors.Count();
            if (colorCount != CodeLength)
            {
                throw new InvalidCodeException($"Code length is not respected. Should be {CodeLength}, given {colorCount}.");
            }

            foreach (Color color in colors)
            {
                if (!Enum.IsDefined(color))
                {
                    throw new WrongColorException($"Unknown color '{color}'.");
                }
            }
        }
    }
}
