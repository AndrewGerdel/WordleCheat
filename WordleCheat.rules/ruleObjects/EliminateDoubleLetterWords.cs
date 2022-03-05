using WordleCheat.interfaces;

namespace WordleCheat.rules;

public class EliminateDoubleLetterWords : IRuleObject
{
    public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem)
    {
        return !wordlistItem.ContainsDoubleLetter;
    }
}

