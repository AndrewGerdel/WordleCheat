using System;
using WordleCheat.exceptions;
using WordleCheat.interfaces;

namespace WordleCheat
{
    public class ScrabbleLetterValueProvider : ILetterValueProvider
    {
        Dictionary<int, List<char>> _charPointMappings = new Dictionary<int, List<char>>();

        public ScrabbleLetterValueProvider()
        {
            _charPointMappings.Add(1, new List<char>() { 'A', 'E', 'I', 'O', 'U', 'L', 'N', 'S', 'T', 'R' });
            _charPointMappings.Add(2, new List<char>() { 'D', 'G' });
            _charPointMappings.Add(3, new List<char>() { 'B', 'C', 'M', 'P' });
            _charPointMappings.Add(4, new List<char>() { 'F', 'H', 'V', 'W', 'Y' });
            _charPointMappings.Add(5, new List<char>() { 'K' });
            _charPointMappings.Add(8, new List<char>() { 'J', 'X' });
            _charPointMappings.Add(10, new List<char>() { 'Q', 'Z' });
        }

        public int GetLetterValue(char letter)
        {
            var kvp = _charPointMappings.FirstOrDefault(x => x.Value.Contains(Char.ToUpper(letter)));
            if (kvp.Value == null)
                throw new UnknownCharacterException(letter);
            return kvp.Key;
        }

        public int GetWordValue(string word)
        {
            int wordValue = 0;
            word.ToCharArray().ToList<char>().ForEach(x =>
            {
                wordValue += GetLetterValue(x);
            });
            return wordValue;
        }
    }
}

