using System;
using WordleCheat.interfaces;

namespace WordleCheat.rules
{
    public class EliminateWordsNotContainingAllOrangeLetters : IRuleObject
    {
        public EliminateWordsNotContainingAllOrangeLetters()
        {
        }

        public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem)
        {
            var orangeLetters = knownLetters.GetOrangeLetters();
            return wordlistItem.ContainsAllLetters(orangeLetters);
        }
    }
}

