using System;
using WordleCheat.interfaces;
using System.Text.RegularExpressions;

namespace WordleCheat.rules
{
    public class LookForDoubleLetters : IRuleObject
    {
        public LookForDoubleLetters()
        {
        }

        public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem)
        {
            var knownLettersMinusUnderscores = knownLetters.GetLettersWithoutUnderscores();
            var hashsetLetters = new HashSet<char>(knownLettersMinusUnderscores);
            if (hashsetLetters.Count() < knownLettersMinusUnderscores.Length)
            {
                var dupeLetter = hashsetLetters.FirstOrDefault(x =>
                {
                    var firstIndex = knownLettersMinusUnderscores.IndexOf(x);
                    var lastIndex = knownLettersMinusUnderscores.LastIndexOf(x);
                    return firstIndex != lastIndex;
                });

                return wordlistItem.ContainsMulitpleInstances(dupeLetter);
                // then there is at least one letter appearing in knownLetters more than once.
            }

            return true;
        }
    }
}

