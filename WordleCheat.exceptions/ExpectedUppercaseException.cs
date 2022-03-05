namespace WordleCheat.exceptions;
public class ExpectedUppercaseException : Exception
{

    char _c;
    public ExpectedUppercaseException(char c)
    {
        _c = c;
    }

    public override string Message => $"Character {_c} is not uppercase.";
}

