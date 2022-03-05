using System;
using WordleCheat.interfaces;
using WordleCheat.exceptions;

namespace WordleCheat
{
    public class WordlistItem : IWordlistItem
    {
        int? _wordScore;
        string _word;
        ILetterValueProvider _letterValueProvider;
        HashSet<char> _uniqueChars;
        List<char> _allChars;

        public string Word => _word;

        public WordlistItem(string word, ILetterValueProvider letterValueProvider)
        {
            _word = word;
            _letterValueProvider = letterValueProvider;
            _allChars = word.ToUpper().ToCharArray().ToList();
            _uniqueChars = new HashSet<char>(_allChars);
        }

        public int WordScore
        {
            get
            {
                return _wordScore != null ? _wordScore.Value : CalculateWordScore();
            }
        }

        private int CalculateWordScore()
        {
            _wordScore = _letterValueProvider.GetWordValue(_word);
            return _wordScore.Value;
        }

        /// <summary>
        /// Returns true if the unique char hashset contains fewer chars than the word,
        /// indicating it contains double letters.
        /// </summary>
        public bool ContainsDoubleLetter
        {
            get
            {
                return _uniqueChars.Count() < _word.Length;
            }
        }

        public bool Hidden { get; set; }

        public override string ToString()
        {
            return $"{Word}({WordScore})";
        }

        /// <summary>
        /// Will only receive capital (placed, green) letters
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool HasPlacedLetterInPosition(char letter, int position)
        {
            if (!Char.IsUpper(letter))
                throw new ExpectedUppercaseException(letter);
            return Char.ToUpper(_allChars[position]).Equals(letter);
        }

        /// <summary>
        /// Returns true if the letter exists in any of the supplied indexes
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public bool DoesLetterExistInIndexes(char letter, params int[] indexes)
        {
            if (Char.IsUpper(letter))
                throw new ExpectedLowercaseException(letter);
            foreach (int index in indexes)
                if (this._allChars[index] == Char.ToUpper(letter))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns true if the word contains any supplied character
        /// </summary>
        /// <param name="letters"></param>
        /// <returns></returns>
        public bool ContainsAnyLetter(params char[] letters)
        {
            foreach (char c in letters)
                if (this.Word.Contains(c, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
        /// <summary>
        /// Returns true if the word contains all supplied letters
        /// </summary>
        /// <param name="letters"></param>
        /// <returns></returns>
        public bool ContainsAllLetters(params char[] letters)
        {
            foreach (char c in letters)
                if (!this.Word.Contains(c, StringComparison.OrdinalIgnoreCase))
                    return false;
            return true;
        }

        /// <summary>
        /// Returns true if the word contains multiple instances of the supplied character
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public bool ContainsMulitpleInstances(char letter)
        {
            letter = Char.ToUpper(letter);
            if (this.Word.ToUpper().IndexOf(letter) != -1 &&
                this.Word.ToUpper().LastIndexOf(letter) != -1 &&
                this.Word.ToUpper().IndexOf(letter) != this.Word.ToUpper().LastIndexOf(letter))
                return true;
            return false;
        }
    }
}

