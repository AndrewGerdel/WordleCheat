using System;
using WordleCheat.interfaces;

namespace WordleCheat.rules
{
    public class LookForPlacedCapitalLetters : IRuleObject
    {
        public LookForPlacedCapitalLetters()
        {
        }

        public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem)
        {
            char[] letters = knownLetters.ToCharArray();
            for (int i = 0; i < letters.Length; i++)
            {
                if (Char.IsUpper(letters[i]))
                {
                    if (!wordlistItem.HasPlacedLetterInPosition(letters[i], i))
                        return false;
                }
            }
            return true;
        }
    }
}

