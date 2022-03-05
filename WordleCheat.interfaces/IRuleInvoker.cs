using System;
namespace WordleCheat.interfaces
{
    public interface IRuleInvoker
    {
        public void ProcessRules(IKnownLettersInput knownLetters);
    }
}

