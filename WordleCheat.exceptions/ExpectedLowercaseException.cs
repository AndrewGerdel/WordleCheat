namespace WordleCheat.exceptions;
public class ExpectedLowercaseException : Exception
{

    char _c;
    public ExpectedLowercaseException(char c)
    {
        _c = c;
    }

    public override string Message => $"Character {_c} is not lowercase.";
}

