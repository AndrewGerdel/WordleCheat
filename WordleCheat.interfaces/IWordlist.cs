using System;
namespace WordleCheat.interfaces
{
    public interface IWordlist
    {
        List<IWordlistItem> ChooseNextWord(IKnownLettersInput knownLetters, string guess, IRuleInvoker ruleInvoker);
        List<IWordlistItem> Items { get; }
        void Remove(IWordlistItem wordlistItem);
        public void UnhideAllWords();
    }
}

