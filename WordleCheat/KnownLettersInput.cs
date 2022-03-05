using System;
using WordleCheat.interfaces;
using System.Text.RegularExpressions;

namespace WordleCheat
{
    public class KnownLettersInput : IKnownLettersInput
    {
        private string _knownInput = String.Empty;
        private char[] _asCharArray = new char[0];
        public string KnownInput { get => _knownInput; }

        public KnownLettersInput()
        {
        }

        public void SetKnownInput(string knownInput)
        {
            _knownInput = knownInput.ToLower();
            _asCharArray = knownInput.ToCharArray();
        }

        public char[] ToCharArray()
        {
            return _asCharArray;
        }

        public char[] GetOrangeLetters()
        {
            List<char> orangeLetters = new List<char>();
            for (int i = 0; i < _asCharArray.Length; i++)
            {
                if (Char.IsLower(_asCharArray[i]))
                    orangeLetters.Add(_asCharArray[i]);
            }
            return orangeLetters.ToArray();
        }

        public string GetLettersWithoutUnderscores()
        {
            return Regex.Replace(_knownInput.ToUpper(), "_", "");
        }
    }
}

