using System;
namespace WordleCheat.interfaces
{
    public interface IWordlistItem
    {
        public string Word { get; }
        public bool ContainsDoubleLetter { get; }
        public int WordScore { get; }
        public bool Hidden { get; set; }
        public bool HasPlacedLetterInPosition(char letter, int position);
        public bool DoesLetterExistInIndexes(char letter, params int[] indexes);
        public bool ContainsAnyLetter(params char[] letters);
        public bool ContainsAllLetters(params char[] letters);
        public bool ContainsMulitpleInstances(char letter);
    }
}

