namespace WordleCheat.interfaces;
public interface ILetterValueProvider
{
    int GetLetterValue(char letter);
    int GetWordValue(string word);
}

