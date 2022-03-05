namespace WordleCheat.exceptions;
public class UnknownCharacterException : Exception
{

    char _c;
    public UnknownCharacterException(char c)
    {
        _c = c;
    }

    public override string Message => $"Character {_c} not recognized";
}

