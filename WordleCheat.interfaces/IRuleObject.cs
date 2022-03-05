using System;
namespace WordleCheat.interfaces
{
    public interface IRuleObject
    {
        public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem);
    }
}

