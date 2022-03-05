using System;
namespace WordleCheat.interfaces
{
    public interface IKnownLettersInput
    {
        public string KnownInput { get; }
        public void SetKnownInput(string knownInput);
        public char[] ToCharArray();
        public char[] GetOrangeLetters();
        public string GetLettersWithoutUnderscores();
    }
}

