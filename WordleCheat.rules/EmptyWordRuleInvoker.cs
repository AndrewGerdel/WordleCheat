using System;
using WordleCheat.interfaces;
using WordleCheat.exceptions;

namespace WordleCheat.rules
{
    public class EmptyWordRuleInvoker : IRuleInvoker
    {
        IWordlist _wordlist;
        List<IRuleObject> _rules;
        public EmptyWordRuleInvoker(IWordlist wordlist)
        {
            _wordlist = wordlist;
            _rules = new List<IRuleObject>() { new EliminateDoubleLetterWords() };
        }

        public void ProcessRules(IKnownLettersInput knownLetters)
        {
            if (!String.IsNullOrEmpty(knownLetters.KnownInput))
                throw new EmptyWordExpectedException(knownLetters.KnownInput);
            _wordlist.Items.ForEach(x =>
            {
                _rules.ForEach(r =>
                {
                    if (!r.Process(knownLetters, x))
                    {
                        _wordlist.Remove(x);
                    }
                });
            });
        }
    }
}

