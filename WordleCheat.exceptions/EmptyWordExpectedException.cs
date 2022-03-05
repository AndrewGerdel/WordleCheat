using System;
namespace WordleCheat.exceptions
{
    public class EmptyWordExpectedException : Exception
    {
        string _knownLetters;
        public EmptyWordExpectedException(string knownLetters)
        {
            _knownLetters = knownLetters;
        }
        public override string Message => $"Expected an empty string. Received {_knownLetters}.";
    }
}

